using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Publisher.Posts;

[Area(PublisherCommonRemoteServiceConsts.ModuleName)]
[RemoteService(Name = PublisherCommonRemoteServiceConsts.RemoteServiceName)]
[Route("api/publisher-common/posts")]
public class PostController : PublisherControllerBase, IPostAppService
{
    protected readonly IPostAppService PostAppService;

    public PostController(IPostAppService postAppService)
    {
        PostAppService = postAppService;
    }

    [HttpGet]
    [Route("by-ids")]
    public async Task<ListResultDto<PostDto>> GetListByIdsAsync(IEnumerable<Guid> ids)
    {
        return await PostAppService.GetListByIdsAsync(ids);
    }
}
