using System;
using System.Threading.Tasks;
using Dignite.Publisher.Admin.Permissions;
using Dignite.Publisher.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Publisher.Admin.Posts;

[Area(PublisherAdminRemoteServiceConsts.ModuleName)]
[RemoteService(Name = PublisherAdminRemoteServiceConsts.RemoteServiceName)]
[Route("api/publisher-admin/posts")]
[Authorize(PublisherAdminPermissions.Posts.Default)]
public class PostAdminController : PublisherAdminController, IPostAdminAppService
{
    protected readonly IPostAdminAppService PostAdminAppService;

    public PostAdminController(IPostAdminAppService postAdminAppService)
    {
        PostAdminAppService = postAdminAppService;
    }

    [HttpPost]
    [Route("{id}/archive")]
    public async Task ArchiveAsync(Guid id)
    {
        await PostAdminAppService.ArchiveAsync(id);
    }

    [HttpPost]
    public async Task<PostDto> CreateAsync(CreatePostInput input)
    {
        return await PostAdminAppService.CreateAsync(input);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task DeleteAsync(Guid id)
    {
        await PostAdminAppService.DeleteAsync(id);
    }

    [HttpPost]
    [Route("{id}/draft")]
    public async Task DraftAsync(Guid id)
    {
        await PostAdminAppService.DraftAsync(id);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<PostDto> GetAsync(Guid id)
    {
        return await PostAdminAppService.GetAsync(id);
    }

    [HttpGet]
    public async Task<PagedResultDto<PostDto>> GetListAsync(GetPostsInput input)
    {
        return await PostAdminAppService.GetListAsync(input);
    }

    [HttpGet]
    [Route("has-post-pending-for-review")]
    public async Task<bool> HasPostPendingForReviewAsync()
    {
        return await PostAdminAppService.HasPostPendingForReviewAsync();
    }

    [HttpPost]
    [Route("{id}/publish")]
    public async Task PublishAsync(Guid id)
    {
        await PostAdminAppService.PublishAsync(id);
    }

    [HttpPost]
    [Route("{id}/send-to-review")]
    public async Task SendToReviewAsync(Guid id)
    {
        await PostAdminAppService.SendToReviewAsync(id);
    }

    [HttpGet]
    [Route("slug-exists")]
    public async Task<bool> SlugExistsAsync(string local, string slug)
    {
        return await PostAdminAppService.SlugExistsAsync(local, slug);
    }

    [HttpPut]
    public async Task<PostDto> UpdateAsync(Guid id, UpdatePostInput input)
    {
        return await  PostAdminAppService.UpdateAsync(id, input);
    }
}
