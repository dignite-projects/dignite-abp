using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Dignite.Abp.TenantDomain.Caddy.CaddyConfig;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.TenantDomain.Caddy;

public class CaddyWebServerManager(
    IHttpClientFactory httpClientFactory,
    ILogger<CaddyWebServerManager> logger,
    CaddyClient client)
    : WebServerManagerBase(logger), ITransientDependency
{
    private const string Path = "apps/http/servers/https";

    public async override Task AddOrUpdateDomainAsync(string domain, string upstreamAddress, Guid tenantId)
    {
        var routeConfig = await FindAsync(tenantId);

        if (routeConfig == null)
        {
            await CreateAsync(new TenantRouteConfig(tenantId, [domain], upstreamAddress));
            return;
        }
        else
        {
            routeConfig.SetMatches(domain);
            // TODO 是否修改下游地址？
            await UpdateAsync(routeConfig);
        }
    }

    public async override Task RemoveDomainAsync(Guid tenantId)
    {
        var id = GetTenantRouteId(tenantId);

        var result = await client.DeleteByIdAsync<string>(id);

        if (result.HttpStatusCode == HttpStatusCode.NotFound)
        {
            return;
        }

        if (!result.IsSuccess)
        {
            throw new UserFriendlyException("Caddy 租户配置删除失败:" + result.ErrorMessage);
        }
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



    public async Task CreateAsync(TenantRouteConfig config)
    {
        var result = await client.CreateConfig<string>(Path + "/routes", config);
        if (!result.IsSuccess)
        {
            throw new UserFriendlyException("Caddy 租户配置修创建败:" + result.ErrorMessage);
        }
    }

    public async Task UpdateAsync(TenantRouteConfig config)
    {
        var id = config.Id;
        var result = await client.UpdateByIdAsync<string>(id, config);

        if (result.HttpStatusCode == HttpStatusCode.NotFound)
        {
            throw new UserFriendlyException("Caddy 不存在的租户配置:" + result.ErrorMessage);
        }

        if (!result.IsSuccess)
        {
            throw new UserFriendlyException("Caddy 租户配置修改失败:" + result.ErrorMessage);
        }
    }

    public async Task<TenantRouteConfig?> FindAsync(Guid? tenantId)
    {
        var id = GetTenantRouteId(tenantId);
        var result = await client.GetByIdAsync<TenantRouteConfig>(id);
        if (result.HttpStatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        if (!result.IsSuccess)
        {
            throw new UserFriendlyException("Caddy " + result.ErrorMessage);
        }

        return result.Data;
    }

    protected string GetTenantRouteId(Guid? tenantId)
    {
        return $"{CaddyConfigConsts.TenantRouteIdPrefix}{tenantId}";
    }
}