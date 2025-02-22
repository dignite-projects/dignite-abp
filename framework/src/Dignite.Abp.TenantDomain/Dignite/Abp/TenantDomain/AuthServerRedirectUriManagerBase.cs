using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Dignite.Abp.TenantDomain;
public abstract class AuthServerRedirectUriManagerBase : IAuthServerRedirectUriManager
{
    protected AuthServerRedirectUriManagerBase(ILogger<AuthServerRedirectUriManagerBase> logger)
    {
        Logger = logger;
    }

    protected ILogger<AuthServerRedirectUriManagerBase> Logger { get; set; }

    public abstract Task AddRedirectDomainAsync(string clientId, string domainName);
    public abstract Task RemoveRedirectDomainAsync(string clientId, string domainName);
}
