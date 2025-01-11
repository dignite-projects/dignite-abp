using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Dignite.Abp.TenantDomains;

[DependsOn(
    typeof(AbpTenantDomainsDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class AbpTenantDomainsApplicationContractsModule : AbpModule
{

}
