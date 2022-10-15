using Dignite.Abp.BlazoriseUI;
using Dignite.Abp.FieldCustomizing.Components;
using Volo.Abp.Modularity;

namespace Dignite.Abp.FieldCustomizing.BlazoriseComponents;

[DependsOn(
    typeof(AbpBlazoriseUiModule),
    typeof(AbpFieldCustomizingComponentsModule)
    )]
public class AbpFieldCustomizingComponentsBlazoriseUIModule : AbpModule
{
}
