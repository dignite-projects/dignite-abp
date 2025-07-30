using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Dignite.Publisher.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace Dignite.Publisher.Posts;
public class MongoPostRepository : MongoDbRepository<IPublisherMongoDbContext, Post, Guid>, IPostRepository
{
    public MongoPostRepository(IMongoDbContextProvider<IPublisherMongoDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task<Post> FindBySlugAsync(string? local, string slug,  CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);

        return await (await GetQueryableAsync(token))
                .FirstOrDefaultAsync(b => b.Local == local && b.Slug == slug, token);
    }

    public virtual async Task<int> GetCountAsync(string? local, IEnumerable<Guid> categoryIds, PostStatus? status = null, string? postType = null, Guid? creatorId = null, DateTime? creationTimeFrom = null, DateTime? creationTimeTo = null, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);

        var query = await GetListQueryAsync(local, categoryIds, status, postType, creatorId, creationTimeFrom, creationTimeTo, token);

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

    public virtual async Task<List<Post>> GetPagedListAsync(string? local, IEnumerable<Guid> categoryIds, PostStatus? status = null, string? postType = null, Guid? creatorId = null, DateTime? creationTimeFrom = null, DateTime? creationTimeTo = null, int skipCount = 0, int maxResultCount = int.MaxValue, string sorting = null, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);

        var query = await GetListQueryAsync(local, categoryIds, status, postType, creatorId, creationTimeFrom, creationTimeTo, token);

        return await query.OrderBy(sorting.IsNullOrEmpty() ? "creationTime desc" : sorting)
                  .PageBy(skipCount, maxResultCount)
                  .ToListAsync(token);
    }

    public virtual async Task<bool> HasPostPendingForReviewAsync(CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);
        return await (await GetQueryableAsync(token)).AnyAsync(x => x.Status == PostStatus.PendingReview, token);
    }

    public virtual async Task<bool> SlugExistsAsync(string? local, string slug, CancellationToken cancellationToken = default)
    {
        var token = GetCancellationToken(cancellationToken);
        return await (await GetQueryableAsync(token)).AnyAsync(x => x.Local == local && x.Slug == slug, token);
    }

    protected virtual async Task<IQueryable<Post>> GetListQueryAsync(string? local, IEnumerable<Guid> categoryIds, PostStatus? status = null, string? postType = null, Guid? creatorId = null, DateTime? creationTimeFrom = null, DateTime? creationTimeTo = null, CancellationToken cancellationToken = default)
    {
        return (await GetQueryableAsync(cancellationToken))
            .Where(p => p.Local == local)
            .WhereIf(categoryIds != null && categoryIds.Count() == 1, b => b.PostCategories.Any(pc => pc.CategoryId == categoryIds.First()))
            .WhereIf(categoryIds != null && categoryIds.Count() > 1, b => b.PostCategories.Any(pc => categoryIds.Contains(pc.CategoryId)))
            .WhereIf(status.HasValue, b => b.Status == status.Value)
            .WhereIf(!string.IsNullOrWhiteSpace(postType), b => b.PostType == postType)
            .WhereIf(creatorId.HasValue, b => b.CreatorId == creatorId.Value)
            .WhereIf(creationTimeFrom.HasValue, b => b.CreationTime >= creationTimeFrom.Value)
            .WhereIf(creationTimeTo.HasValue, b => b.CreationTime <= creationTimeTo.Value);
    }
}
