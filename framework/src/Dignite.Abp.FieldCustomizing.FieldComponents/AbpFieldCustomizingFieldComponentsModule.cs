using Dignite.Abp.BlazoriseUI;
using Volo.Abp.Modularity;

namespace Dignite.Abp.FieldCustomizing.FieldComponents;

[DependsOn(
    typeof(AbpBlazoriseUIModule),
    typeof(AbpFieldCustomizingModule)
    )]
public class AbpFieldCustomizingFieldComponentsModule : AbpModule
{

}
