using Dignite.Abp.LocaleManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Abp.LocaleManagement;

public abstract class LocaleManagementController : AbpControllerBase
{
    protected LocaleManagementController()
    {
        LocalizationResource = typeof(LocaleManagementResource);
    }
}
