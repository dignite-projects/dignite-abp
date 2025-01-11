using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.Modularity;

namespace Dignite.Abp.TenantDomains.AspNetCore;

[DependsOn(
    typeof(AbpTenantDomainsApplicationContractsModule),
    typeof(AbpAspNetCoreMultiTenancyModule)
    )]
public class AbpTenantDomainsAspNetCoreModule : AbpModule
{
}
