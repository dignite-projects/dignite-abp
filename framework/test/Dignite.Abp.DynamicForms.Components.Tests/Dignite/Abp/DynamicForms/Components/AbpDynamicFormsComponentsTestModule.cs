using Dignite.Abp.DynamicForms.Components.BlazoriseUI;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Dignite.Abp.DynamicForms.Components;

[DependsOn(
    typeof(AbpTestBaseModule),
    typeof(AbpAutofacModule),
    typeof(AbpDynamicFormsComponentsBlazoriseUiModule)
    )]
public class AbpDynamicFormsComponentsTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}