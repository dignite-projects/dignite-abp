using System.Threading.Tasks;

namespace Dignite.Abp.TenantDomain;

/// <summary>
/// Manages the redirect domains for authentication and post-logout 
/// in authorization servers such as IdentityServer and OpenIddict.
///
/// This interface allows adding and removing redirect domains, 
/// with the actual redirect URIs being automatically constructed 
/// based on predefined rules.
///
/// This service is essential for multi-tenant SaaS applications that support custom domain binding.
/// </summary>
public interface IAuthServerRedirectUriManager
{
    /// <summary>
    /// Adds a new redirect domain for a given client.
    /// The actual redirect URIs will be constructed automatically.
    /// </summary>
    /// <param name="clientId">The ID of the client.</param>
    /// <param name="domainName">The domain to be used for redirect URIs.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AddRedirectDomainAsync(string clientId, string domainName);

    /// <summary>
    /// Removes an existing redirect domain for a given client.
    /// The associated redirect URIs will be removed automatically.
    /// </summary>
    /// <param name="clientId">The ID of the client.</param>
    /// <param name="domainName">The domain to be removed from redirect URIs.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task RemoveRedirectDomainAsync(string clientId, string domainName);
}
