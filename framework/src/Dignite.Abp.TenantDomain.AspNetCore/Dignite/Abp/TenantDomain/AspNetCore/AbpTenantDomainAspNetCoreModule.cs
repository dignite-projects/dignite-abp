using Dignite.Abp.TenantDomain.WebServer;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.Modularity;

namespace Dignite.Abp.TenantDomainManagement.AspNetCore;

[DependsOn(
    typeof(AbpTenantDomainModule),
    typeof(AbpAspNetCoreMultiTenancyModule)
    )]
public class AbpTenantDomainAspNetCoreModule : AbpModule
{
}
