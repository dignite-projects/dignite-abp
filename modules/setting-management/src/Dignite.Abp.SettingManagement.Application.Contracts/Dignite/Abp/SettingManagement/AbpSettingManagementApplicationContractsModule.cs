using Dignite.Abp.DynamicForms;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Dignite.Abp.SettingManagement;

[DependsOn(
    typeof(AbpSettingManagementDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationAbstractionsModule),
    typeof(AbpDynamicFormsModule)
)]
public class AbpSettingManagementApplicationContractsModule : AbpModule
{
}