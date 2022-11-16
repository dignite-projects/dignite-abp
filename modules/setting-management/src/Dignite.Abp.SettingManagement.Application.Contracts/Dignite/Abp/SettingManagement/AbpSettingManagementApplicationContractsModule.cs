using Dignite.Abp.Settings.DynamicForms;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Dignite.Abp.SettingManagement;

[DependsOn(
    typeof(AbpSettingManagementDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationAbstractionsModule),
    typeof(AbpSettingsDynamicFormsModule)
)]
public class AbpSettingManagementApplicationContractsModule : AbpModule
{
}