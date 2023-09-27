using Volo.Abp.Modularity;

namespace Dignite.Abp.DynamicForms.Components;

[DependsOn(
    typeof(DigniteAbpDynamicFormsModule)
    )]
public class DigniteAbpDynamicFormsComponentsModule : AbpModule
{
}