using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Publisher.Posts;
public interface IPostRepository : IBasicRepository<Post, Guid>
{
    Task<Post> FindBySlugAsync(string? local, string slug, CancellationToken cancellationToken = default);

    Task<int> GetCountAsync(
        string? local,
        IEnumerable<Guid> categoryIds,
        PostStatus? status = null,
        string? postType = null,
        Guid? creatorId = null,
        DateTime? creationTimeFrom = null,
        DateTime? creationTimeTo = null,
        CancellationToken cancellationToken = default
        );

    Task<List<Post>> GetPagedListAsync(
        string? local,
        IEnumerable<Guid> categoryIds,
        PostStatus? status = null,
        string? postType = null,
        Guid? creatorId = null,
        DateTime? creationTimeFrom = null,
        DateTime? creationTimeTo = null,
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        string sorting = null,
        CancellationToken cancellationToken = default);

    Task<List<Post>> GetListByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    Task<List<Post>> GetListByCategoryIdAsync(
        Guid categoryId,
        CancellationToken cancellationToken = default);

    Task<bool> SlugExistsAsync(string? local, string slug, CancellationToken cancellationToken = default);

    Task<bool> HasPostPendingForReviewAsync(CancellationToken cancellationToken = default);
}
