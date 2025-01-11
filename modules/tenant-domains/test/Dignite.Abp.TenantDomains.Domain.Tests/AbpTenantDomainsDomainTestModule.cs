using Volo.Abp.Modularity;

namespace Dignite.Abp.TenantDomains;

[DependsOn(
    typeof(AbpTenantDomainsDomainModule),
    typeof(AbpTenantDomainsTestBaseModule)
)]
public class AbpTenantDomainsDomainTestModule : AbpModule
{

}
