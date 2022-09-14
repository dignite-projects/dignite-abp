using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;

namespace Dignite.Abp.SettingManagement;

[DependsOn(
    typeof(AbpDddApplicationModule),
    typeof(AbpSettingManagementDomainModule),
    typeof(AbpSettingManagementApplicationContractsModule)
)]
public class AbpSettingManagementApplicationModule : AbpModule
{
}