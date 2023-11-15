using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dignite.CmsKit;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(DigniteCmsKitDomainSharedModule)
)]
public class DigniteCmsKitDomainModule : AbpModule
{

}
