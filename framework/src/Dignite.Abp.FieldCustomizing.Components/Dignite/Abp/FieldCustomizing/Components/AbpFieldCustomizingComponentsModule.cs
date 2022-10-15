using Volo.Abp.Modularity;

namespace Dignite.Abp.FieldCustomizing.Components;

[DependsOn(
    typeof(AbpFieldCustomizingModule)
    )]
public class AbpFieldCustomizingComponentsModule : AbpModule
{
}