using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Dignite.Cms.Admin.Sections
{
    public interface ISectionAdminAppService
    : ICrudAppService<
        SectionDto,
        Guid,
        GetSectionsInput,
        CreateSectionInput,
        UpdateSectionInput>
    {
        Task<bool> NameExistsAsync(SectionNameExistsInput input);
        Task<bool> RouteExistsAsync(SectionRouteExistsInput input);
    }
}
