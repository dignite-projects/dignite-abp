using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace Dignite.Abp.TenantDomain.TenantRouteConfigs;

public class ReverseProxyRouteHandle : HandleBase
{
    public ReverseProxyRouteHandle()
    {

    }

    public ReverseProxyRouteHandle(Guid tenantId, string upstreamAddress)
    {
        Routes.Add(new HandleRoute(tenantId, upstreamAddress));
    }

    [JsonProperty("handler")]
    public override string Handler { get; set; } = "subroute";

    [JsonProperty("routes")]
    public List<HandleRoute> Routes { get; set; } = [];
}
