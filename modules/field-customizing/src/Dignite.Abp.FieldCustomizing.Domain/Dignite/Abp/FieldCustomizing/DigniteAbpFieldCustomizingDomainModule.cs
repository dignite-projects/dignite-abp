using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dignite.Abp.FieldCustomizing;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(DigniteAbpFieldCustomizingDomainSharedModule)
)]
public class DigniteAbpFieldCustomizingDomainModule : AbpModule
{
}