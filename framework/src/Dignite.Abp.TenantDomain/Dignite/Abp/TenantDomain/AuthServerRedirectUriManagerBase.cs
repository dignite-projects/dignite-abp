using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Dignite.Abp.TenantDomain;
public abstract class AuthServerRedirectUriManagerBase(ILogger<AuthServerRedirectUriManagerBase> logger)
    : IAuthServerRedirectUriManager
{
    protected ILogger<AuthServerRedirectUriManagerBase> Logger { get; set; } = logger;

    public abstract Task AddRedirectDomainAsync(string clientId, string domainName);
    public abstract Task RemoveRedirectDomainAsync(string clientId, string domainName);
}
