using Dignite.Abp.BlazoriseUI;
using Volo.Abp.Modularity;

namespace Dignite.Abp.FieldCustomizing.Components.BlazoriseUI;

[DependsOn(
    typeof(AbpBlazoriseUiModule),
    typeof(AbpFieldCustomizingComponentsModule)
    )]
public class AbpFieldCustomizingComponentsBlazoriseUiModule : AbpModule
{
}
