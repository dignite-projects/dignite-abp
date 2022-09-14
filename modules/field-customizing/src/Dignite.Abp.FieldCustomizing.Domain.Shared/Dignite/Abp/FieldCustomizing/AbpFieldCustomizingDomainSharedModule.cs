using Volo.Abp.EventBus;
using Volo.Abp.Modularity;

namespace Dignite.Abp.FieldCustomizing;

[DependsOn(
    typeof(AbpEventBusModule),
    typeof(AbpFieldCustomizingModule)
)]
public class AbpFieldCustomizingDomainSharedModule : AbpModule
{
}