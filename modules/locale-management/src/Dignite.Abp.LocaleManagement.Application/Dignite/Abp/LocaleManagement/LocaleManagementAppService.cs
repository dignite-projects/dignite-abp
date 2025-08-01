using Dignite.Abp.LocaleManagement.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.LocaleManagement;

public abstract class LocaleManagementAppService : ApplicationService
{
    protected LocaleManagementAppService()
    {
        LocalizationResource = typeof(LocaleManagementResource);
        ObjectMapperContext = typeof(AbpLocaleManagementApplicationModule);
    }
}
