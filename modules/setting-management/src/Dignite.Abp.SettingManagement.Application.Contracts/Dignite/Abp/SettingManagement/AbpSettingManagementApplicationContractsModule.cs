using Dignite.Abp.FieldCustomizing;
using Dignite.Abp.SettingsGrouping;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Dignite.Abp.SettingManagement;

[DependsOn(
    typeof(AbpSettingManagementDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationAbstractionsModule),
    typeof(AbpFieldCustomizingModule),
    typeof(AbpSettingsGroupingModule)
)]
public class AbpSettingManagementApplicationContractsModule : AbpModule
{
}
