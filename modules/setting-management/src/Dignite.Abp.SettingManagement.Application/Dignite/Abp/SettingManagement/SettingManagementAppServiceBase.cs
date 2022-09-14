using Dignite.Abp.SettingManagement.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.SettingManagement;

public class SettingManagementAppServiceBase : ApplicationService
{
    protected SettingManagementAppServiceBase()
    {
        ObjectMapperContext = typeof(AbpSettingManagementApplicationModule);
        LocalizationResource = typeof(AbpSettingManagementResource);
    }
}