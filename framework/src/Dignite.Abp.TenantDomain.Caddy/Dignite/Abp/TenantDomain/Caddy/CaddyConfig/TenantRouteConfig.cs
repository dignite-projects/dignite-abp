using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Dignite.Abp.TenantDomain.Caddy.CaddyConfig;

public class TenantRouteConfig
{
    public TenantRouteConfig(Guid tenantId, string[] hosts, string upstreamAddress)
    {
        Id = $"{CaddyConfigConsts.TenantRouteIdPrefix}{tenantId}";
        SetMatches(hosts);
        Handles = [new ReverseProxyRouteHandle(tenantId, upstreamAddress)];
    }

    [JsonProperty("@id")]
    public string Id { get; private set; }

    [JsonProperty("terminal")]
    public bool Terminal { get; private set; } = true;

    [JsonProperty("handle")]
    public List<ReverseProxyRouteHandle> Handles { get; private set; }

    [JsonProperty("match")]
    public List<RouteMatch> Matches { get; private set; }

    public void SetMatches(params string[] hosts)
    {
        if (Matches.IsNullOrEmpty())
        {
            Matches = hosts.Select(host => new RouteMatch { Hosts = { host } }).ToList();
        }

        foreach (var host in hosts)
        {
            if (!Matches.Any(m => m.Hosts.Contains(host)))
            {
                Matches.Add(new RouteMatch { Hosts = { host } });
            }
        }
    }

    public Guid GetTenantId()
    {
        return Guid.Parse(Id.RemovePreFix(CaddyConfigConsts.TenantRouteIdPrefix));
    }
}
