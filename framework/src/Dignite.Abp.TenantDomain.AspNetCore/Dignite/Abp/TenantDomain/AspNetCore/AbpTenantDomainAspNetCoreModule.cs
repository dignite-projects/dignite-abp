using Dignite.Abp.TenantDomain.WebServer;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.TenantDomainManagement.AspNetCore;

[DependsOn(
    typeof(AbpTenantDomainModule),
    typeof(AbpAspNetCoreMultiTenancyModule)
    )]
public class AbpTenantDomainAspNetCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpTenantResolveOptions>(options =>
        {
            options.AddProxyHeaderTenantResolver();
        });
    }
}
