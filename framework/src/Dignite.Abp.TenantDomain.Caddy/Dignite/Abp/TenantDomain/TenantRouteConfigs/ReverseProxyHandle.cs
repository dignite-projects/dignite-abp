using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;

namespace Dignite.Abp.TenantDomain.TenantRouteConfigs;

public class ReverseProxyHandle : HandleBase
{
    public ReverseProxyHandle()
    {
    }

    public ReverseProxyHandle(Guid tenantId, string upstreamAddress)
    {

        Headers = new HeaderRequest(tenantId);

        if (upstreamAddress.StartsWith("https://"))
        {
            var uri = new Uri(upstreamAddress);
            Headers.Request!.SetHost(uri.Host);
            Upstreams.Add(new Upstream { Dial = $"{uri.Host}:{uri.Port}" });
            Transport = new HandleTransport();
            return;
        }

        if (upstreamAddress.StartsWith("http://"))
        {
            var uri = new Uri(upstreamAddress);
            Upstreams.Add(new Upstream { Dial = $"{uri.Host}:{uri.Port}" });
            return;
        }

        Upstreams.Add(new Upstream { Dial = upstreamAddress });
    }

    [JsonProperty("handler")]
    public override string Handler { get; set; } = "reverse_proxy";

    [JsonProperty("headers")]
    public HeaderRequest Headers { get; set; }

    [JsonProperty("transport", NullValueHandling = NullValueHandling.Ignore)]
    public HandleTransport? Transport { get; set; }

    [JsonProperty("upstreams")]
    public List<Upstream> Upstreams { get; set; } = [];

    public class HeaderRequest
    {
        [JsonProperty("request")]
        public RequestHeaderSet? Request { get; set; }

        public HeaderRequest()
        {
        }

        public HeaderRequest(Guid tenantId)
        {
            Request = new RequestHeaderSet().SetTenantId(tenantId);
        }
    }

    public class RequestHeaderSet
    {
        [JsonProperty("set")]
        public Dictionary<string, object> Set { get; set; } = [];

        public RequestHeaderSet SetTenantId(Guid tenantId)
        {
            Set["__tenant"] = new[] { tenantId.ToString() };

            return this;
        }

        public Guid? GetTenantId()
        {
            if (!Set.TryGetValue("__tenant", out var tenantIds))
            {
                return null;
            }

            return tenantIds switch
            {
                string[] tenantIdArray => Guid.Parse(tenantIdArray[0]),
                JArray tenantIdJArray => Guid.Parse(tenantIdJArray[0].Value<string>()!),
                _ => null
            };
        }

        public RequestHeaderSet SetHost(string host)
        {
            Set["Host"] = new[] { host };

            return this;
        }
    }

    public class Upstream
    {
        [JsonProperty("dial")]
        public string Dial { get; set; } = null!;
    }

    public class HandleTransport
    {
        [JsonProperty("protocol")]
        public string Protocol { get; set; } = "http";

        [JsonProperty("tls")]
        public Dictionary<string, string> Tls = new();
    }
}