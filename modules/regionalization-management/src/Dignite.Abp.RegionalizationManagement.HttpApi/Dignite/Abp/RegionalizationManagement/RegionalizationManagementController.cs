using Dignite.Abp.RegionalizationManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Abp.RegionalizationManagement;

public abstract class RegionalizationManagementController : AbpControllerBase
{
    protected RegionalizationManagementController()
    {
        LocalizationResource = typeof(RegionalizationManagementResource);
    }
}
