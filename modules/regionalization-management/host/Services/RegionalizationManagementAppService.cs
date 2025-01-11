using Dignite.Abp.RegionalizationManagement.Host.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.RegionalizationManagement.Host.Services;

/* Inherit your application services from this class. */
public abstract class RegionalizationManagementAppService : ApplicationService
{
    protected RegionalizationManagementAppService()
    {
        LocalizationResource = typeof(RegionalizationManagementHostResource);
    }
}