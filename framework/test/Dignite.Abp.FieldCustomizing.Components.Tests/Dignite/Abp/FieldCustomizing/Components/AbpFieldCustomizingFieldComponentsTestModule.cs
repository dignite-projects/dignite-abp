using Dignite.Abp.FieldCustomizing.Components.BlazoriseUI;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Dignite.Abp.FieldCustomizing.Components;

[DependsOn(
    typeof(AbpTestBaseModule),
    typeof(AbpAutofacModule),
    typeof(AbpFieldCustomizingComponentsBlazoriseUiModule)
    )]
public class AbpFieldCustomizingFieldComponentsTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}