using System;
using System.Threading.Tasks;
using Dignite.Publisher.Categories;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Publisher.Public.Categories;

[Area(PublisherPublicRemoteServiceConsts.ModuleName)]
[RemoteService(Name = PublisherPublicRemoteServiceConsts.RemoteServiceName)]
[Route("api/publisher-public/categories")]
public class CategoryPublicController : PublisherPublicController, ICategoryPublicAppService
{
    protected readonly ICategoryPublicAppService CategoryPublicAppService;

    public CategoryPublicController(ICategoryPublicAppService categoryPublicAppService)
    {
        CategoryPublicAppService = categoryPublicAppService;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<CategoryDto> GetAsync(Guid id)
    {
        return await CategoryPublicAppService.GetAsync(id);
    }

    [HttpGet]
    public async Task<ListResultDto<CategoryDto>> GetListAsync(string? locale)
    {
        return await CategoryPublicAppService.GetListAsync(locale);
    }
}
