using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace Dignite.Abp.TenantDomain.TenantRouteConfigs;

public class HandleRoute
{
    public HandleRoute()
    {

    }

    public HandleRoute(Guid tenantId, string upstreamAddress)
    {
        Handles.Add(new ReverseProxyHandle(tenantId, upstreamAddress));
    }

    [JsonProperty("handle")]
    public List<ReverseProxyHandle> Handles { get; set; } = [];
}
