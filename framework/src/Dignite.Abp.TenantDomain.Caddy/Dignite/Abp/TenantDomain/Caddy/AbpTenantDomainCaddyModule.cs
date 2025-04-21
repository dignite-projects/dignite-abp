using Caddy;
using Caddy.Client;
using Dignite.Abp.TenantDomain.WebServer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.Modularity;

namespace Dignite.Abp.TenantDomain.Caddy;

[DependsOn(
    typeof(AbpTenantDomainModule), 
    typeof(AbpAspNetCoreMultiTenancyModule))]
public class AbpTenantDomainCaddyModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        Configure<CaddyOptions>(configuration.GetSection("Caddy"));

        context.Services.TryAddSingleton(service =>
        {
            var option = service.GetRequiredService<IOptions<CaddyOptions>>().Value;
            return new CaddyClient(option.Endpoint, option.Username, option.Password);
        });
    }
}
