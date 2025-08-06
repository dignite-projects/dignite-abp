using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Dignite.Publisher.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Polly;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using Volo.CmsKit.Blogs;
using Volo.CmsKit.MarkedItems;
using Volo.CmsKit.Tags;
using Volo.CmsKit.Users;

namespace Dignite.Publisher.Posts;
public class MongoPostRepository : MongoDbRepository<IPublisherMongoDbContext, Post, Guid>, IPostRepository
{
    private readonly MarkedItemManager _markedItemManager;
    private EntityTagManager _entityTagManager;
    public MongoPostRepository(
        IMongoDbContextProvider<IPublisherMongoDbContext> dbContextProvider,
        MarkedItemManager markedItemManager,
        EntityTagManager entityTagManager) : base(dbContextProvider)
    {
        _markedItemManager = markedItemManager;
        _entityTagManager = entityTagManager;
    }

    public virtual async Task<Post> GetBySlugAsync(string? locale, string slug,  CancellationToken cancellationToken = default)
    {
        Check.NotNullOrEmpty(slug, nameof(slug));

        var token = GetCancellationToken(cancellationToken);

        var post = await GetAsync(x =>
                x.Locale == locale &&
                x.Slug.ToLower() == slug,
            cancellationToken: token);

        post.Creator = await (await GetQueryableAsync<CmsUser>(token)).FirstOrDefaultAsync(x => x.Id == post.CreatorId, token);

        return post;
    }

    public virtual async Task<int> GetCountAsync(string? locale, IEnumerable<Guid> categoryIds, PostStatus? status = null, string? postType = null, Guid? creatorId = null,
        Guid? tagId = null,
        Guid? favoriteUserId = null, DateTime? creationTimeFrom = null, DateTime? creationTimeTo = null, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);

        var query = await GetListQueryAsync(locale, categoryIds, status, postType, creatorId, tagId,favoriteUserId, creationTimeFrom, creationTimeTo, token);

        return await query.CountAsync(token);
    }

    public async Task<List<Post>> GetListByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);
        return await (await GetQueryableAsync(token))
                .Where(x => x.PostCategories.Any(pc => pc.CategoryId == categoryId))
                .ToListAsync(token);
    }

    public virtual async Task<List<Post>> GetListByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);

        return await (await GetQueryableAsync(token))
                .Where(b => ids.Contains(b.Id))
                .ToListAsync(token);
    }

    public virtual async Task<List<Post>> GetPagedListAsync(string? locale, IEnumerable<Guid> categoryIds, PostStatus? status = null, string? postType = null, Guid? creatorId = null,
        Guid? tagId = null,
        Guid? favoriteUserId = null, DateTime? creationTimeFrom = null, DateTime? creationTimeTo = null, int skipCount = 0, int maxResultCount = int.MaxValue, string sorting = null, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);
        var dbContext = await GetDbContextAsync(cancellationToken);
        var usersQueryable = dbContext.Collection<CmsUser>().AsQueryable();

        var queryable = await GetListQueryAsync(locale, categoryIds, status, postType, creatorId, tagId, favoriteUserId, creationTimeFrom, creationTimeTo, token);


        queryable = queryable.OrderBy(sorting.IsNullOrEmpty() ? $"{nameof(BlogPost.CreationTime)} desc" : sorting);

        var combinedQueryable = queryable
                                .Join(
                                    usersQueryable,
                                    o => o.CreatorId,
                                    i => i.Id,
                                    (post, user) => new { post, user })
                                .Skip(skipCount)
                                .Take(maxResultCount);

        var combinedResult = await AsyncExecuter.ToListAsync(combinedQueryable, cancellationToken);

        return combinedResult.Select(s =>
        {
            s.post.Creator = s.user;
            return s.post;
        }).ToList();
    }

    public virtual async Task<bool> HasPostPendingForReviewAsync(CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);
        return await (await GetQueryableAsync(token)).AnyAsync(x => x.Status == PostStatus.PendingReview, token);
    }

    public virtual async Task<bool> SlugExistsAsync(string? locale, string slug, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);
        return await (await GetQueryableAsync(token)).AnyAsync(x => x.Locale == locale && x.Slug == slug, token);
    }

    public virtual async Task<List<CmsUser>> GetCreatorsHasPostsAsync(int skipCount, int maxResultCount, string sorting, CancellationToken cancellationToken = default)
    {
        var queryable = (await CreateAuthorsQueryableAsync(cancellationToken))
                        .Skip(skipCount)
                        .Take(maxResultCount)
                        .OrderBy(sorting.IsNullOrEmpty() ? nameof(CmsUser.UserName) : sorting);

        return await AsyncExecuter.ToListAsync(queryable, GetCancellationToken(cancellationToken));
    }

    public virtual async Task<int> GetCreatorsHasPostsCountAsync(CancellationToken cancellationToken = default)
    {
        return await AsyncExecuter.CountAsync(
            (await CreateAuthorsQueryableAsync(cancellationToken)));
    }

    protected virtual async Task<IQueryable<CmsUser>> CreateAuthorsQueryableAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);

        var postQueryable = (await GetQueryableAsync());

        var usersQueryable = (await GetDbContextAsync(cancellationToken)).Collection<CmsUser>().AsQueryable();

        return postQueryable
                        .Join(
                            usersQueryable,
                            o => o.CreatorId,
                            i => i.Id,
                            (post, user) => new { post, user })
                        .Select(s => s.user)
                        .Distinct();
    }

    protected virtual async Task<IQueryable<Post>> GetListQueryAsync(string? locale, IEnumerable<Guid> categoryIds, PostStatus? status = null, string? postType = null, Guid? creatorId = null,
        Guid? tagId = null,
        Guid? favoriteUserId = null, DateTime? creationTimeFrom = null, DateTime? creationTimeTo = null, CancellationToken cancellationToken = default)
    {
        var tagFilteredEntityIds = await GetEntityIdsByTagId(tagId, cancellationToken);

        var favoriteUserFilteredEntityIds = await GetFavoriteEntityIdsByUserId(favoriteUserId, cancellationToken);

        return (await GetQueryableAsync(cancellationToken))
            .Where(p => p.Locale == locale)
            .WhereIf(categoryIds != null && categoryIds.Count() == 1, b => b.PostCategories.Any(pc => pc.CategoryId == categoryIds.First()))
            .WhereIf(categoryIds != null && categoryIds.Count() > 1, b => b.PostCategories.Any(pc => categoryIds.Contains(pc.CategoryId)))
            .WhereIf(status.HasValue, b => b.Status == status.Value)
            .WhereIf(!string.IsNullOrWhiteSpace(postType), b => b.PostType == postType)
            .WhereIf(creatorId.HasValue, b => b.CreatorId == creatorId.Value)
            .WhereIf(tagFilteredEntityIds.Any(), x => tagFilteredEntityIds.Contains(x.Id))
            .WhereIf(favoriteUserFilteredEntityIds.Any(), x => favoriteUserFilteredEntityIds.Contains(x.Id))
            .WhereIf(creationTimeFrom.HasValue, b => b.CreationTime >= creationTimeFrom.Value)
            .WhereIf(creationTimeTo.HasValue, b => b.CreationTime <= creationTimeTo.Value);
    }

    protected virtual async Task<List<Guid>> GetEntityIdsByTagId(Guid? tagId, CancellationToken cancellationToken)
    {
        var entityIdFilters = new List<Guid>();
        if (!tagId.HasValue)
        {
            return entityIdFilters;
        }

        var entityIds =
            await _entityTagManager.GetEntityIdsFilteredByTagAsync(tagId.Value, CurrentTenant.Id, cancellationToken);

        foreach (var entityId in entityIds)
        {
            if (Guid.TryParse(entityId, out var parsedEntityId))
            {
                entityIdFilters.Add(parsedEntityId);
            }
        }

        return entityIdFilters;
    }

    protected virtual async Task<List<Guid>> GetFavoriteEntityIdsByUserId(Guid? userId, CancellationToken cancellationToken)
    {
        var entityIdFilters = new List<Guid>();
        if (!userId.HasValue)
        {
            return entityIdFilters;
        }

        var entityIds =
            await _markedItemManager.GetEntityIdsFilteredByUserAsync(userId.Value, BlogPostConsts.EntityType, CurrentTenant.Id, cancellationToken);

        foreach (var entityId in entityIds)
        {
            if (Guid.TryParse(entityId, out var parsedEntityId))
            {
                entityIdFilters.Add(parsedEntityId);
            }
        }

        return entityIdFilters;
    }
}
