using Newtonsoft.Json;

namespace Dignite.Abp.TenantDomain.TenantRouteConfigs;

public abstract class HandleBase
{
    [JsonProperty("handler")]
    public virtual string Handler { get; set; } = null!;
}
