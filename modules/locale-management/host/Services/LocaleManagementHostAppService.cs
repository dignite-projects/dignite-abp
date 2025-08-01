using Dignite.Abp.LocaleManagement.Host.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.LocaleManagement.Host.Services;

/* Inherit your application services from this class. */
public abstract class LocaleManagementHostAppService : ApplicationService
{
    protected LocaleManagementHostAppService()
    {
        LocalizationResource = typeof(LocaleManagementHostResource);
    }
}