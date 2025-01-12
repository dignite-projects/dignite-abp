using Volo.Abp.Modularity;

namespace Dignite.Abp.DynamicForms.Components;

[DependsOn(
    typeof(AbpDynamicFormsModule)
    )]
public class AbpDynamicFormsComponentsModule : AbpModule
{
}