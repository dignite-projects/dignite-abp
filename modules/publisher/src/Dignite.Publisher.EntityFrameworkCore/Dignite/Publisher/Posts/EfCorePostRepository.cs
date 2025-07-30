using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Dignite.Publisher.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Publisher.Posts;
public class EfCorePostRepository : EfCoreRepository<IPublisherDbContext, Post, Guid>, IPostRepository
{
    public EfCorePostRepository(IDbContextProvider<IPublisherDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task<Post> FindBySlugAsync(string? local, string slug, CancellationToken cancellationToken = default)
    {
        return await (await GetQueryableAsync())
            .IncludeDetails()
            .FirstOrDefaultAsync(x => x.Local == local && x.Slug == slug, GetCancellationToken(cancellationToken));
    }

    public virtual async Task<int> GetCountAsync(string? local, IEnumerable<Guid> categoryIds, PostStatus? status = null, string? postType = null, Guid? creatorId = null, DateTime? creationTimeFrom = null, DateTime? creationTimeTo = null, CancellationToken cancellationToken = default)
    {
        return await (await GetListQueryAsync(local, categoryIds, status, postType, creatorId, creationTimeFrom, creationTimeTo))
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

    public virtual async Task<List<Post>> GetPagedListAsync(string? local, IEnumerable<Guid> categoryIds, PostStatus? status = null, string? postType = null, Guid? creatorId = null, DateTime? creationTimeFrom = null, DateTime? creationTimeTo = null, int skipCount = 0, int maxResultCount = int.MaxValue, string sorting = null, CancellationToken cancellationToken = default)
    {
        var query = await GetListQueryAsync(local, categoryIds, status, postType, creatorId, creationTimeFrom, creationTimeTo);

        return await query
            .IncludeDetails()
            .OrderBy(sorting.IsNullOrEmpty() ? $"{nameof(Post.CreationTime)} desc" : sorting)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<bool> HasPostPendingForReviewAsync(CancellationToken cancellationToken = default)
    {
        return await (await GetQueryableAsync())
            .AnyAsync(x => x.Status == PostStatus.PendingReview, GetCancellationToken(cancellationToken));
    }

    public virtual async Task<bool> SlugExistsAsync(string? local, string slug, CancellationToken cancellationToken = default)
    {
        return await (await GetQueryableAsync())
            .AnyAsync(x => x.Local == local && x.Slug == slug, GetCancellationToken(cancellationToken));
    }

    protected virtual async Task<IQueryable<Post>> GetListQueryAsync(string? local, IEnumerable<Guid> categoryIds, PostStatus? status = null, string? postType = null, Guid? creatorId = null, DateTime? creationTimeFrom = null, DateTime? creationTimeTo = null)
    {
        return (await GetQueryableAsync())
            .Where(p => p.Local == local)
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
