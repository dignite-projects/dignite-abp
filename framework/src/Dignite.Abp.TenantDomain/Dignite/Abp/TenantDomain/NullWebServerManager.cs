using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Dignite.Abp.TenantDomain;
public class NullWebServerManager : WebServerManagerBase
{
    public NullWebServerManager(ILogger<WebServerManagerBase> logger) : base(logger)
    {
    }

    public override Task AddOrUpdateDomainAsync(string domain, string upstreamAddress, Guid tenantId, string site = null)
    {
        Logger.LogWarning("USING NullWebServerManager!");
        Logger.LogDebug("AddOrUpdateDomainAsync:");
        Logger.LogDebug(domain);
        Logger.LogDebug(upstreamAddress);
        Logger.LogDebug(site);
        return Task.FromResult(0);
    }

    public override Task<bool> CheckCertificateValidityAsync(string domain)
    {
        Logger.LogWarning("USING NullWebServerManager!");
        Logger.LogDebug("CheckCertificateValidityAsync:");
        Logger.LogDebug(domain);
        return Task.FromResult(false);
    }

    public override Task RemoveDomainAsync(Guid tenantId, string site = null)
    {
        Logger.LogWarning("USING NullWebServerManager!");
        Logger.LogDebug("RemoveDomainAsync");
        Logger.LogDebug(tenantId.ToString());
        Logger.LogDebug(site);
        return Task.FromResult(0);
    }
}
