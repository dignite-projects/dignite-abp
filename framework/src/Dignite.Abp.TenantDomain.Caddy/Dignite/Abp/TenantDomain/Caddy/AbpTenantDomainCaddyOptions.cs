using System.Net.Http;

namespace Dignite.Abp.TenantDomain.Caddy;
public class AbpTenantDomainCaddyOptions
{
    public string ApiEndpoint { get; set; } = "http://localhost:2019";


    /// <summary>
    /// Can be set to a value if you want to use a named <see cref="HttpClient"/> instance
    /// while creating it from <see cref="IHttpClientFactory"/>.
    /// Default value: "" (<see cref="Microsoft.Extensions.Options.Options.DefaultName"/>).
    /// </summary>
    public string HttpClientName { get; } = Microsoft.Extensions.Options.Options.DefaultName;
}
