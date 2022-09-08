using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dignite.Abp.FieldCustomizing;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AbpFieldCustomizingDomainSharedModule)
)]
public class AbpFieldCustomizingDomainModule : AbpModule
{

}
