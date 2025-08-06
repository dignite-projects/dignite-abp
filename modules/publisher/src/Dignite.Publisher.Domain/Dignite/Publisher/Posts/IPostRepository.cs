using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.CmsKit.Users;

namespace Dignite.Publisher.Posts;
public interface IPostRepository : IBasicRepository<Post, Guid>
{
    Task<Post> GetBySlugAsync(string? locale, [NotNull] string slug, CancellationToken cancellationToken = default);

    Task<int> GetCountAsync(
        string? locale,
        IEnumerable<Guid> categoryIds,
        PostStatus? status = null,
        string? postType = null,
        Guid? creatorId = null,
        Guid? tagId = null,
        Guid? favoriteUserId = null,
        DateTime? creationTimeFrom = null,
        DateTime? creationTimeTo = null,
        CancellationToken cancellationToken = default
        );

    Task<List<Post>> GetPagedListAsync(
        string? locale,
        IEnumerable<Guid> categoryIds,
        PostStatus? status = null,
        string? postType = null,
        Guid? creatorId = null,
        Guid? tagId = null,
        Guid? favoriteUserId = null,
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

    Task<bool> SlugExistsAsync(string? locale, string slug, CancellationToken cancellationToken = default);

    Task<bool> HasPostPendingForReviewAsync(CancellationToken cancellationToken = default);

    Task<List<CmsUser>> GetCreatorsHasPostsAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        CancellationToken cancellationToken = default);

    Task<int> GetCreatorsHasPostsCountAsync( CancellationToken cancellationToken = default);
}
