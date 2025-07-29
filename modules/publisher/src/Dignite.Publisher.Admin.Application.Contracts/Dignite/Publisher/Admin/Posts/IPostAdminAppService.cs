using System;
using System.Threading.Tasks;
using Dignite.Publisher.Posts;
using Volo.Abp.Application.Services;

namespace Dignite.Publisher.Admin.Posts;

public interface IPostAdminAppService: ICrudAppService<PostDto, Guid, GetPostsInput, CreatePostDto, UpdatePostDto>
{
    Task<bool> SlugExistsAsync(string? local, string slug);

    Task DraftAsync(Guid id);
    Task SendToReviewAsync(Guid id);
    Task<bool> HasPostPendingForReviewAsync();
    Task PublishAsync(Guid id);
    Task ArchiveAsync(Guid id);
}
