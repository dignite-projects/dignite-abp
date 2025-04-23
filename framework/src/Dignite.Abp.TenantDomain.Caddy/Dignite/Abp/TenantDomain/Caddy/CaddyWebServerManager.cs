using System;
using System.Net.Http;
using System.Threading.Tasks;
using Dignite.Abp.TenantDomain.TenantRouteConfigs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.TenantDomain.Caddy;
public class CaddyWebServerManager : WebServerManagerBase, ITransientDependency
{
    private readonly IHttpClientFactory _httpClientFactory;

    protected readonly CaddyTenantConfigManager _caddyTenantConfigManager;

    private readonly AbpTenantDomainCaddyOptions _options;
    private readonly AbpAspNetCoreMultiTenancyOptions _multiTenancyOptions;

    public CaddyWebServerManager(
        IHttpClientFactory httpClientFactory,
        ILogger<CaddyWebServerManager> logger,
        IOptions<AbpTenantDomainCaddyOptions> options,
        IOptions<AbpAspNetCoreMultiTenancyOptions> multiTenancyOptions, CaddyTenantConfigManager caddyTenantConfigManager) : base(logger)
    {
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
        _multiTenancyOptions = multiTenancyOptions.Value;
        _caddyTenantConfigManager = caddyTenantConfigManager;
    }

    public override Task AddOrUpdateDomainAsync(string domain, string upstreamAddress, Guid tenantId, string? site = null)
    {
        return _caddyTenantConfigManager.AddOrUpdateAsync(new TenantRouteConfig(tenantId, [domain], upstreamAddress));
    }

    public override Task RemoveDomainAsync(Guid tenantId, string? site = null)
    {
        return _caddyTenantConfigManager.DeleteAsync(tenantId);
    }

    public async override Task<bool> CheckCertificateValidityAsync(string domain)
    {
        var client = _httpClientFactory.CreateClient();
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