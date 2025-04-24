using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Dignite.Abp.TenantDomain;
public abstract class WebServerManagerBase(ILogger<WebServerManagerBase> logger) : IWebServerManager
{
    protected ILogger<WebServerManagerBase> Logger { get; set; } = logger;

    public abstract Task AddOrUpdateDomainAsync(string domain, string upstreamAddress, Guid tenantId);
    public abstract Task<bool> CheckCertificateValidityAsync(string domain);
    public abstract Task RemoveDomainAsync(Guid tenantId);
}
