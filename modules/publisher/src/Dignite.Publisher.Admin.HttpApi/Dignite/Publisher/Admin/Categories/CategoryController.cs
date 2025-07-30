using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dignite.Publisher.Admin.Permissions;
using Dignite.Publisher.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Publisher.Admin.Categories;

[Area(PublisherAdminRemoteServiceConsts.ModuleName)]
[RemoteService(Name = PublisherAdminRemoteServiceConsts.RemoteServiceName)]
[Route("api/publisher-admin/categories")]
[Authorize(PublisherAdminPermissions.Categories.Default)]
public class CategoryController : PublisherAdminController, ICategoryAdminAppService
{
    protected readonly ICategoryAdminAppService CategoryAdminAppService;

    public CategoryController(ICategoryAdminAppService categoryAdminAppService)
    {
        CategoryAdminAppService = categoryAdminAppService;
    }

    [HttpPost]
    public async Task<CategoryDto> CreateAsync(CreateCategoryInput input)
    {
        return await CategoryAdminAppService.CreateAsync(input);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task DeleteAsync(Guid id)
    {
        await CategoryAdminAppService.DeleteAsync(id);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<CategoryDto> GetAsync(Guid id)
    {
        return await CategoryAdminAppService.GetAsync(id);
    }

    [HttpGet]
    public async Task<PagedResultDto<CategoryDto>> GetListAsync(GetCategoriesInput input)
    {
        return await CategoryAdminAppService.GetListAsync(input);
    }

    [HttpPost]
    [Route("{id}/move")]
    public async Task MoveAsync(Guid id, MoveCategoryInput input)
    {
        await CategoryAdminAppService.MoveAsync(id, input);
    }

    [HttpGet]
    [Route("name-exists")]
    public async Task<bool> NameExistsAsync(Guid? parentId, string name)
    {
        return await CategoryAdminAppService.NameExistsAsync(parentId, name);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<CategoryDto> UpdateAsync(Guid id, UpdateCategoryInput input)
    {
        return await CategoryAdminAppService.UpdateAsync(id, input);
    }
}
