using Dignite.Abp.DynamicForms;
using Volo.Abp.EventBus;
using Volo.Abp.Modularity;

namespace Dignite.Abp.FieldCustomizing;

[DependsOn(
    typeof(AbpEventBusModule),
    typeof(DigniteAbpDynamicFormsModule)
)]
public class DigniteAbpFieldCustomizingDomainSharedModule : AbpModule
{
}