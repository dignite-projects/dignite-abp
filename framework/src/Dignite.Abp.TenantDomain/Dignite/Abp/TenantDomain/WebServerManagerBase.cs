using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Dignite.Abp.TenantDomain;
public abstract class WebServerManagerBase : IWebServerManager
{
    protected WebServerManagerBase(ILogger<WebServerManagerBase> logger)
    {
        Logger = logger;
    }

    protected ILogger<WebServerManagerBase> Logger { get; set; }

    public abstract Task AddOrUpdateDomainAsync(string domain, string upstreamAddress, Guid tenantId, string site = null);
    public abstract Task<bool> CheckCertificateValidityAsync(string domain);
    public abstract Task RemoveDomainAsync(Guid tenantId, string site = null);
}
