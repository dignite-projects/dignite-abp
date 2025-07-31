using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Dignite.Publisher.Admin.Posts;

public interface IPostAdminAppService: ICrudAppService<PostAdminDtoBase, Guid, GetPostsInput, CreatePostInput, UpdatePostInput>
{
    Task<bool> SlugExistsAsync(string? local, string slug);

    Task DraftAsync(Guid id);
    Task SendToReviewAsync(Guid id);
    Task<bool> HasPostPendingForReviewAsync();
    Task PublishAsync(Guid id);
    Task ArchiveAsync(Guid id);
}
