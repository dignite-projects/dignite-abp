using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Dignite.Abp.FieldCustomizing;

[DependsOn(
    typeof(AbpTestBaseModule),
    typeof(AbpAutofacModule),
    typeof(AbpFieldCustomizingModule)
    )]
public class AbpFieldCustomizingTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}