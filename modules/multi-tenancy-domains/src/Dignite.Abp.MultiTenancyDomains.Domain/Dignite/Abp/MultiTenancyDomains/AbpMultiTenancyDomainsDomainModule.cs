using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dignite.Abp.MultiTenancyDomains;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AbpMultiTenancyDomainsDomainSharedModule)
)]
public class AbpMultiTenancyDomainsDomainModule : AbpModule
{

}
