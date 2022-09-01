
using Volo.Abp.Application.Services;
using Volo.Abp.SettingManagement.Localization;

namespace Dignite.Abp.SettingManagement
{
    public class SettingManagementAppServiceBase : ApplicationService
    {
        protected SettingManagementAppServiceBase()
        {
            ObjectMapperContext = typeof(DigniteAbpSettingManagementApplicationModule);
            LocalizationResource = typeof(AbpSettingManagementResource);
        }
    }
}
