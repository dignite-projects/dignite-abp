using Volo.Abp.BlazoriseUI;
using Volo.Abp.Modularity;

namespace Dignite.Abp.DynamicForms.Components.BlazoriseUI;

[DependsOn(
    typeof(AbpBlazoriseUIModule),
    typeof(AbpDynamicFormsComponentsModule)
    )]
public class AbpDynamicFormsComponentsBlazoriseUiModule : AbpModule
{
}
