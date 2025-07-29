using System;
using System.Threading.Tasks;
using Dignite.Publisher.Categories;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.Publisher.Public.Categories;
public interface ICategoryPublicAppService: IApplicationService
{
    /// <summary>
    /// Get a category by its ID.   
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<CategoryDto> GetAsync(Guid id);

    /// <summary>
    /// Get a category by its local identifier.
    /// </summary>
    /// <param name="local"></param>
    /// <returns>
    /// Return a list of multi-level structures
    /// </returns>
    Task<ListResultDto<CategoryDto>> GetListAsync(string? local);
}
