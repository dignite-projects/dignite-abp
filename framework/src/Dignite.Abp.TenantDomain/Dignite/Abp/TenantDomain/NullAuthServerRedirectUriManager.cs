using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Dignite.Abp.TenantDomain;
public class NullAuthServerRedirectUriManager : AuthServerRedirectUriManagerBase
{
    public NullAuthServerRedirectUriManager(ILogger<AuthServerRedirectUriManagerBase> logger) : base(logger)
    {
    }

    public override Task AddRedirectDomainAsync(string clientId, string domainName)
    {
        Logger.LogWarning("USING NullAuthServerRedirectUriManager!");
        Logger.LogDebug("AddRedirectDomainAsync:");
        Logger.LogDebug("ClientId:"+clientId);
        Logger.LogDebug("Domain Name:" + domainName);
        return Task.FromResult(0);
    }
    public override Task RemoveRedirectDomainAsync(string clientId, string domainName)
    {
        Logger.LogWarning("USING NullAuthServerRedirectUriManager!");
        Logger.LogDebug("RemoveRedirectDomainAsync:");
        Logger.LogDebug("ClientId:" + clientId);
        Logger.LogDebug("Domain Name:" + domainName);
        return Task.FromResult(0);
    }
}
