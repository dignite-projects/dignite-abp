using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dignite.Publisher.Categories;
using Volo.Abp.Application.Dtos;

namespace Dignite.Publisher.Public.Categories;
public class CategoryPublicAppService : PublisherPublicAppService, ICategoryPublicAppService
{
    public ICategoryRepository CategoryRepository;
    protected CategoryManager CategoryManager;

    public CategoryPublicAppService(ICategoryRepository categoryRepository, CategoryManager categoryManager)
    {
        CategoryRepository = categoryRepository;
        CategoryManager = categoryManager;
    }

    public async Task<CategoryDto> GetAsync(Guid id)
    {
        var category = await CategoryRepository.GetAsync(id);
        return ObjectMapper.Map<Category, CategoryDto>(category);
    }

    public async Task<ListResultDto<CategoryDto>> GetListAsync(string? locale)
    {
        var list = await CategoryManager.GetTreeListAsync(locale);
        return new ListResultDto<CategoryDto>(
            ObjectMapper.Map<List<Category>, List<CategoryDto>>(list)
        );
    }
}
