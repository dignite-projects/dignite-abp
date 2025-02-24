using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.TenantDomain.Caddy;
public class CaddyWebServerManager : WebServerManagerBase, ITransientDependency
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly AbpTenantDomainCaddyOptions _options;

    public CaddyWebServerManager(
        IHttpClientFactory httpClientFactory,
        ILogger<CaddyWebServerManager> logger,
        IOptions<AbpTenantDomainCaddyOptions> options) : base(logger)
    {
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
    }

    public override async Task AddOrUpdateDomainAsync(string domain, string upstreamAddress, Guid tenantId, string? site = null)
    {
        var client = _httpClientFactory.CreateClient(_options.HttpClientName);
        var configJson = await GetCurrentConfigJson(client);

        // 使用 System.Text.Json 的 JsonNode 直接操作 JSON
        var config = JsonNode.Parse(configJson)?.AsObject();
        if (config == null) throw new BusinessException("CaddyManagement:InvalidConfig");

        var servers = config["apps"]?["http"]?["servers"]?.AsObject();
        var siteConfig = site != null ? servers?[site] : servers?["default"];
        if (siteConfig == null) throw new BusinessException("CaddyManagement:001");

        // 直接修改 JSON 配置
        UpdateSiteConfigJson(siteConfig.AsObject(), domain, upstreamAddress, tenantId);
        await UpdateConfigJson(client, config.ToJsonString());
    }

    public override async Task RemoveDomainAsync(string domain, string? site = null)
    {
        var client = _httpClientFactory.CreateClient(_options.HttpClientName);
        var configJson = await GetCurrentConfigJson(client);

        var config = JsonNode.Parse(configJson)?.AsObject();
        if (config == null) throw new BusinessException("CaddyManagement:InvalidConfig");

        var servers = config["apps"]?["http"]?["servers"]?.AsObject();
        var siteConfig = site != null ? servers?[site] : servers?["default"];
        if (siteConfig == null) throw new BusinessException("CaddyManagement:002");

        // 直接移除域名相关的路由
        RemoveDomainFromSiteConfigJson(siteConfig.AsObject(), domain);
        await UpdateConfigJson(client, config.ToJsonString());
    }

    public override async Task<bool> CheckCertificateValidityAsync(string domain)
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

    private async Task<string> GetCurrentConfigJson(HttpClient client)
    {
        var response = await client.GetAsync($"{_options.ApiEndpoint}/config/");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    private async Task UpdateConfigJson(HttpClient client, string jsonConfig)
    {
        var content = new StringContent(jsonConfig, Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"{_options.ApiEndpoint}/load", content);
        response.EnsureSuccessStatusCode();
    }

    private void UpdateSiteConfigJson(JsonObject siteConfig, string domain, string upstreamAddress, Guid tenantId)
    {
        // 设置监听端口
        siteConfig["listen"] = JsonValue.Create(new[] { ":443" });

        // 获取或初始化 routes 数组
        var routesNode = siteConfig["routes"]?.AsArray();
        if (routesNode == null)
        {
            routesNode = new JsonArray();
            siteConfig["routes"] = routesNode;
        }

        // 查找现有路由
        var routeNode = routesNode.FirstOrDefault(r => r?["match"]?[0]?["host"]?.AsArray().Any(h => h?.ToString() == domain) == true);

        if (routeNode == null)
        {
            // 创建新路由
            routeNode = JsonNode.Parse($@"{{
                ""match"": [{{""host"": [""{domain}""]}}],
                ""handle"": [{{
                    ""handler"": ""reverse_proxy"",
                    ""upstreams"": [{{""dial"": ""{upstreamAddress}""}}],
                    ""headers"": {{
                        ""request"": {{
                            ""set"": {{
                                ""{WebServerConsts.ProxyHeaderTenantId}"": [""{tenantId}""]
                            }}
                        }}
                    }}
                }}]
            }}");
            routesNode.Add(routeNode);
        }
        else
        {
            // 更新现有路由
            var handle = routeNode["handle"]?.AsArray()[0]?.AsObject();
            if (handle != null)
            {
                handle["upstreams"] = JsonNode.Parse($@"[{{""dial"": ""{upstreamAddress}""}}]");
                handle["headers"] = JsonNode.Parse($@"{{
                    ""request"": {{
                        ""set"": {{
                            ""{WebServerConsts.ProxyHeaderTenantId}"": [""{tenantId}""]
                        }}
                    }}
                }}");
            }
        }
    }

    private void RemoveDomainFromSiteConfigJson(JsonObject siteConfig, string domain)
    {
        var routesNode = siteConfig["routes"]?.AsArray();
        if (routesNode == null) return;

        // 移除匹配域名的路由
        var filteredRoutes = routesNode
            .Where(r => r?["match"]?[0]?["host"]?.AsArray().Any(h => h?.ToString() == domain) != true)
            .ToArray();

        siteConfig["routes"] = new JsonArray(filteredRoutes);
    }
}