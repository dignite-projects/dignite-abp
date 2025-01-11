using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dignite.Abp.TenantDomains;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AbpTenantDomainsDomainSharedModule)
)]
public class AbpTenantDomainsDomainModule : AbpModule
{

}
