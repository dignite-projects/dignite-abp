using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Dignite.Abp.MultiTenancyDomains;

[DependsOn(
    typeof(AbpMultiTenancyDomainsDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class AbpMultiTenancyDomainsApplicationContractsModule : AbpModule
{

}
