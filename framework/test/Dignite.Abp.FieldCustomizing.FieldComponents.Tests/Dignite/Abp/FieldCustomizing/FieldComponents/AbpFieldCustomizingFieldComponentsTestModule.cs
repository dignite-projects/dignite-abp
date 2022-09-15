using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Dignite.Abp.FieldCustomizing.FieldComponents;

[DependsOn(
    typeof(AbpTestBaseModule),
    typeof(AbpAutofacModule),
    typeof(AbpFieldCustomizingFieldComponentsModule)
    )]
public class AbpFieldCustomizingFieldComponentsTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}