using System;
using System.Threading.Tasks;
using Dignite.Publisher.Categories;

namespace Dignite.Publisher.Admin.Categories;
public interface ICategoryAdminAppService : Volo.Abp.Application.Services.ICrudAppService<
    CategoryDto,
    Guid,
    GetCategoriesInput,
    CreateCategoryInput,
    UpdateCategoryInput>
{
    Task MoveAsync(Guid id, MoveCategoryInput input);

    Task<bool> NameExistsAsync(Guid? parentId, string name);
}
