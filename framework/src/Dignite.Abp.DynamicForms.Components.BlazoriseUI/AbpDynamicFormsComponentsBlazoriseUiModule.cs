using Dignite.Abp.BlazoriseUI;
using Volo.Abp.Modularity;

namespace Dignite.Abp.DynamicForms.Components.BlazoriseUI;

[DependsOn(
    typeof(DigniteAbpBlazoriseUiModule),
    typeof(AbpDynamicFormsComponentsModule)
    )]
public class AbpDynamicFormsComponentsBlazoriseUiModule : AbpModule
{
}
