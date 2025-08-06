using System;
using System.Threading.Tasks;
using Dignite.Publisher.Posts;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.CmsKit.Public.Comments;

namespace Dignite.Publisher.Public.Posts;

[Area(PublisherPublicRemoteServiceConsts.ModuleName)]
[RemoteService(Name = PublisherPublicRemoteServiceConsts.RemoteServiceName)]
[Route("api/publisher-public/posts")]
public class PostPublicController : PublisherPublicController, IPostPublicAppService
{
    protected readonly IPostPublicAppService PostPublicAppService;

    public PostPublicController(IPostPublicAppService postPublicAppService)
    {
        PostPublicAppService = postPublicAppService;
    }

    [HttpGet]
    [Route("{locale}/{postSlug}")]
    public async Task<PostDtoBase> GetAsync([CanBeNull] string? locale, [NotNull] string postSlug)
    {
        return await PostPublicAppService.GetAsync(locale, postSlug);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<PostDtoBase> GetAsync(Guid id)
    {
        return await PostPublicAppService.GetAsync(id);
    }

    [HttpGet]
    [Route("authors")]
    public async Task<PagedResultDto<CmsUserDto>> GetCreatorsHasPostsAsync(PagedAndSortedResultRequestDto input)
    {
        return await PostPublicAppService.GetCreatorsHasPostsAsync(input);
    }

    [HttpGet]
    public async Task<PagedResultDto<PostDtoBase>> GetListAsync(GetPostsInput input)
    {
        return await PostPublicAppService.GetListAsync(input);
    }
}
