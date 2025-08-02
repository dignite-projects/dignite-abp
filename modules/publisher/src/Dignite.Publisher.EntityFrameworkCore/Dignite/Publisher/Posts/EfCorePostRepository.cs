using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Dignite.Publisher.EntityFrameworkCore;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.CmsKit.Blogs;
using Volo.CmsKit.Users;

namespace Dignite.Publisher.Posts;
public class EfCorePostRepository : EfCoreRepository<IPublisherDbContext, Post, Guid>, IPostRepository
{
    public EfCorePostRepository(IDbContextProvider<IPublisherDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task<Post> GetBySlugAsync(string? locale, [NotNull] string slug, CancellationToken cancellationToken = default)
    {
        Check.NotNullOrEmpty(slug, nameof(slug));

        var post = await GetAsync(
                                x => x.Locale == locale && x.Slug.ToLower() == slug,
                                cancellationToken: GetCancellationToken(cancellationToken));

        post.Creator = await (await GetDbContextAsync())
                            .Set<CmsUser>()
                            .FirstOrDefaultAsync(x => x.Id == post.CreatorId, GetCancellationToken(cancellationToken));

        return post;
    }

    public virtual async Task<int> GetCountAsync(string? locale, IEnumerable<Guid> categoryIds, PostStatus? status = null, string? postType = null, Guid? creatorId = null, DateTime? creationTimeFrom = null, DateTime? creationTimeTo = null, CancellationToken cancellationToken = default)
    {
        return await (await GetListQueryAsync(locale, categoryIds, status, postType, creatorId, creationTimeFrom, creationTimeTo))
            .CountAsync(GetCancellationToken(cancellationToken));
    }

    public async Task<List<Post>> GetListByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        return await(await GetQueryableAsync()).Where(x => x.PostCategories.Any(pc=>pc.CategoryId==categoryId))
            .IncludeDetails()
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<Post>> GetListByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        return await (await GetQueryableAsync()).Where(x=>ids.Contains(x.Id))
            .IncludeDetails()
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<Post>> GetPagedListAsync(string? locale, IEnumerable<Guid> categoryIds, PostStatus? status = null, string? postType = null, Guid? creatorId = null, DateTime? creationTimeFrom = null, DateTime? creationTimeTo = null, int skipCount = 0, int maxResultCount = int.MaxValue, string sorting = null, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        var usersDbSet = dbContext.Set<CmsUser>();
        var queryable = await GetListQueryAsync(locale, categoryIds, status, postType, creatorId, creationTimeFrom, creationTimeTo);
        queryable = queryable.OrderBy(sorting.IsNullOrEmpty() ? $"{nameof(BlogPost.CreationTime)} desc" : sorting);

        var combinedResult = await queryable
            .Join(
                usersDbSet,
                o => o.CreatorId,
                i => i.Id,
                (post, user) => new { post, user })
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));

        return combinedResult.Select(s =>
        {
            s.post.Creator = s.user;
            return s.post;
        }).ToList();
    }

    public virtual async Task<bool> HasPostPendingForReviewAsync(CancellationToken cancellationToken = default)
    {
        return await (await GetQueryableAsync())
            .AnyAsync(x => x.Status == PostStatus.PendingReview, GetCancellationToken(cancellationToken));
    }

    public virtual async Task<bool> SlugExistsAsync(string? locale, string slug, CancellationToken cancellationToken = default)
    {
        return await (await GetQueryableAsync())
            .AnyAsync(x => x.Locale == locale && x.Slug == slug, GetCancellationToken(cancellationToken));
    }
    public virtual async Task<List<CmsUser>> GetCreatorsHasPostsAsync(int skipCount, int maxResultCount, string sorting, CancellationToken cancellationToken = default)
    {
        return await (await CreateAuthorsQueryableAsync())
            .Skip(skipCount)
            .Take(maxResultCount)
            .OrderBy(sorting.IsNullOrEmpty() ? nameof(CmsUser.UserName) : sorting)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<int> GetCreatorsHasPostsCountAsync(CancellationToken cancellationToken = default)
    {
        return await (await CreateAuthorsQueryableAsync())
            .CountAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual async Task<IQueryable<CmsUser>> CreateAuthorsQueryableAsync()
    {
        return (await GetDbContextAsync()).Posts.Select(x => x.Creator).Distinct();
    }

    protected virtual async Task<IQueryable<Post>> GetListQueryAsync(string? locale, IEnumerable<Guid> categoryIds, PostStatus? status = null, string? postType = null, Guid? creatorId = null, DateTime? creationTimeFrom = null, DateTime? creationTimeTo = null)
    {
        return (await GetQueryableAsync())
            .Where(p => p.Locale == locale)
            .WhereIf(categoryIds != null && categoryIds.Count() == 1, b => b.PostCategories.Any(pc => pc.CategoryId == categoryIds.First()))
            .WhereIf(categoryIds != null && categoryIds.Count() > 1, b => b.PostCategories.Any(pc => categoryIds.Contains(pc.CategoryId)))
            .WhereIf(status.HasValue, b => b.Status == status.Value)
            .WhereIf(!string.IsNullOrWhiteSpace(postType), b => b.PostType == postType)
            .WhereIf(creatorId.HasValue, b => b.CreatorId == creatorId.Value)
            .WhereIf(creationTimeFrom.HasValue, b => b.CreationTime >= creationTimeFrom.Value)
            .WhereIf(creationTimeTo.HasValue, b => b.CreationTime <= creationTimeTo.Value);
    }
    public override async Task<IQueryable<Post>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}
