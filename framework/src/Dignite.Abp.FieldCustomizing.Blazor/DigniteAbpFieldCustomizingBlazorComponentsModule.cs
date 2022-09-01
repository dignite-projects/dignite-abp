using Volo.Abp.BlazoriseUI;
using Volo.Abp.Modularity;

namespace Dignite.Abp.FieldCustomizing.Blazor
{
    [DependsOn(
        typeof(AbpBlazoriseUIModule),
        typeof(DigniteAbpFieldCustomizingModule)
        )]
    public class DigniteAbpFieldCustomizingBlazorComponentsModule : AbpModule
    {
        
    }
}