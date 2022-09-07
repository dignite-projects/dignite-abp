using Dignite.Abp.BlazoriseUI;
using Volo.Abp.Modularity;

namespace Dignite.Abp.FieldCustomizing.Blazor
{
    [DependsOn(
        typeof(DigniteAbpBlazoriseUIModule),
        typeof(AbpFieldCustomizingModule)
        )]
    public class AbpFieldCustomizingBlazorComponentsModule : AbpModule
    {
        
    }
}