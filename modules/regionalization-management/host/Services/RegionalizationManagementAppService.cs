using Dignite.Abp.RegionalizationManagement.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.RegionalizationManagement.Services;

/* Inherit your application services from this class. */
public abstract class RegionalizationManagementAppService : ApplicationService
{
    protected RegionalizationManagementAppService()
    {
        LocalizationResource = typeof(RegionalizationManagementResource);
    }
}