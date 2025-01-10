using Dignite.Abp.RegionalizationManagement.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.RegionalizationManagement;

public abstract class RegionalizationManagementAppService : ApplicationService
{
    protected RegionalizationManagementAppService()
    {
        LocalizationResource = typeof(RegionalizationManagementResource);
        ObjectMapperContext = typeof(RegionalizationManagementApplicationModule);
    }
}
