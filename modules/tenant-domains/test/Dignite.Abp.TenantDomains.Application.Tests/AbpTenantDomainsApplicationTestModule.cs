using Volo.Abp.Modularity;

namespace Dignite.Abp.TenantDomains;

[DependsOn(
    typeof(AbpTenantDomainsApplicationModule),
    typeof(AbpTenantDomainsDomainTestModule)
    )]
public class AbpTenantDomainsApplicationTestModule : AbpModule
{

}
