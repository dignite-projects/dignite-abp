
using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;

namespace Dignite.Abp.SettingManagement
{
    [DependsOn(
        typeof(AbpDddApplicationModule),
        typeof(AbpSettingManagementDomainModule),
        typeof(DigniteAbpSettingManagementApplicationContractsModule)
    )]
    public class DigniteAbpSettingManagementApplicationModule : AbpModule
    {
    }
}
