using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.Modularity;

namespace Dignite.Abp.MultiTenancyDomains.AspNetCore;

[DependsOn(
    typeof(AbpMultiTenancyDomainsApplicationContractsModule),
    typeof(AbpAspNetCoreMultiTenancyModule)
    )]
public class AbpMultiTenancyDomainsAspNetCoreModule : AbpModule
{
}
