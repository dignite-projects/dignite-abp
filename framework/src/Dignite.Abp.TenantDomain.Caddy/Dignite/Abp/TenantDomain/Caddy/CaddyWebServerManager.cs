using System;
using System.Net.Http;
using System.Threading.Tasks;
using Dignite.Abp.TenantDomain.TenantRouteConfigs;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.TenantDomain.Caddy;

public class CaddyWebServerManager(
    IHttpClientFactory httpClientFactory,
    ILogger<CaddyWebServerManager> logger,
    CaddyTenantConfigManager caddyTenantConfigManager)
    : WebServerManagerBase(logger), ITransientDependency
{
    public async override Task AddOrUpdateDomainAsync(string domain, string upstreamAddress, Guid tenantId)
    {
        var routeConfig = await caddyTenantConfigManager.FindAsync(tenantId);

        if (routeConfig == null)
        {
            await caddyTenantConfigManager.CreateAsync(new TenantRouteConfig(tenantId, [domain], upstreamAddress));
            return;
        }

        routeConfig.SetMatches(domain);
        // TODO 是否修改下游地址？
        await caddyTenantConfigManager.UpdateAsync(routeConfig);
    }

    public override Task RemoveDomainAsync(Guid tenantId)
    {
        return caddyTenantConfigManager.DeleteAsync(tenantId);
    }

    public async override Task<bool> CheckCertificateValidityAsync(string domain)
    {
        var client = httpClientFactory.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Head, $"https://{domain}");

        try
        {
            var response = await client.SendAsync(request);
            return response.IsSuccessStatusCode && response.RequestMessage.RequestUri.Scheme == "https";
        }
        catch (HttpRequestException)
        {
            return false;
        }
    }
}