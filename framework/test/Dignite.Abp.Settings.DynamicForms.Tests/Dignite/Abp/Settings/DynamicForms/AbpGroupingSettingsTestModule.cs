using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Settings.DynamicForms;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpSettingsDynamicFormsModule),
    typeof(AbpTestBaseModule)
    )]
public class AbpSettingsGroupingTestModule : AbpModule
{
}