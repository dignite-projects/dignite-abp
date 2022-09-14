using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Dignite.Abp.SettingsGrouping;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpSettingsGroupingModule),
    typeof(AbpTestBaseModule)
    )]
public class AbpSettingsGroupingTestModule : AbpModule
{
}