using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dignite.Publisher.Admin.Permissions;
using Dignite.Publisher.Categories;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;

namespace Dignite.Publisher.Admin.Categories;

[Authorize(PublisherAdminPermissions.Categories.Default)]
public class CategoryAdminAppService : PublisherAdminAppService, ICategoryAdminAppService
{
    protected ICategoryRepository CategoryRepository;
    protected CategoryManager CategoryManager;

    public CategoryAdminAppService(ICategoryRepository categoryRepository, CategoryManager categoryManager)
    {
        CategoryRepository = categoryRepository;
        CategoryManager = categoryManager;
    }

    [Authorize(PublisherAdminPermissions.Categories.Create)]
    public async Task<CategoryDto> CreateAsync(CreateCategoryDto input)
    {
        var category = await CategoryManager.CreateAsync(
            input.Local,
            input.ParentId,
            input.DisplayName,
            input.Name,
            input.Description,
            input.IsActive,
            input.PostTypes,
            input.Order
        );

        return ObjectMapper.Map<Category, CategoryDto>(category);
    }

    [Authorize(PublisherAdminPermissions.Categories.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await CategoryManager.DeleteAsync(id);
    }

    public async Task<CategoryDto> GetAsync(Guid id)
    {
        var category = await CategoryRepository.GetAsync(id);
        return ObjectMapper.Map<Category, CategoryDto>(category);
    }

    public async Task<PagedResultDto<CategoryDto>> GetListAsync(GetCategoriesInput input)
    {
        var list = await CategoryRepository.GetListAsync(input.Local);
        return new PagedResultDto<CategoryDto>(
            list.Count,
            ObjectMapper.Map<List<Category>, List<CategoryDto>>(list)
        );
    }

    [Authorize(PublisherAdminPermissions.Categories.Update)]
    public async Task MoveAsync(Guid id, MoveCategoryInput input)
    {
        var category = await CategoryRepository.GetAsync(id);
        await CategoryManager.MoveAsync(
            category,
            input.ParentId,
            input.Order
        );

        await CategoryRepository.UpdateAsync(category);
    }

    public Task<bool> NameExistsAsync(Guid? parentId, string name)
    {
        return CategoryRepository.NameExistsAsync(parentId, name);
    }

    [Authorize(PublisherAdminPermissions.Categories.Update)]
    public async Task<CategoryDto> UpdateAsync(Guid id, UpdateCategoryDto input)
    {
        var category = await CategoryRepository.GetAsync(id);
        if (category.Name != input.Name)
        {
            await CategoryManager.CheckNameExistenceAsync(category.ParentId, input.Name);
        }

        category.SetDisplayName(input.DisplayName);
        category.SetName(input.Name);
        category.IsActive = input.IsActive;
        category.SetDescription(input.Description);
        foreach (var postType in input.PostTypes)
        {
            if (!category.PostTypes.Contains(postType))
            {
                category.AddPostType(postType);
            }
        }
        foreach (var postType in category.PostTypes)
        {
            if (!input.PostTypes.Contains(postType))
            {
                category.RemovePostType(postType);
            }
        }

        await CategoryRepository.UpdateAsync(category);
        return ObjectMapper.Map<Category, CategoryDto>(category);
    }
}
