using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dignite.Abp.TenantDomain.Caddy.CaddyConfig;

public class RouteMatch
{
    [JsonProperty("host")]
    public List<string> Hosts { get; private set; } = [];
}
