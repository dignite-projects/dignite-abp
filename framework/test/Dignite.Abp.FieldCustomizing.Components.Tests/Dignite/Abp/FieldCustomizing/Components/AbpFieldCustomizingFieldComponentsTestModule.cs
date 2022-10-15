using Dignite.Abp.FieldCustomizing.BlazoriseComponents;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Dignite.Abp.FieldCustomizing.Components;

[DependsOn(
    typeof(AbpTestBaseModule),
    typeof(AbpAutofacModule),
    typeof(AbpFieldCustomizingComponentsBlazoriseUIModule)
    )]
public class AbpFieldCustomizingFieldComponentsTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}