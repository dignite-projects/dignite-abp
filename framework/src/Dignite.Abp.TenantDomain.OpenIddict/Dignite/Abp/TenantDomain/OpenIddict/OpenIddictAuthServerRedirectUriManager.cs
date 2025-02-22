using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OpenIddict.Abstractions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.OpenIddict.Applications;

namespace Dignite.Abp.TenantDomain.OpenIddict;
public class OpenIddictAuthServerRedirectUriManager : AuthServerRedirectUriManagerBase, ITransientDependency
{
    protected IOpenIddictApplicationManager ApplicationManager;
    public OpenIddictAuthServerRedirectUriManager(
        IOpenIddictApplicationManager applicationManager,
        ILogger<AuthServerRedirectUriManagerBase> logger) : base(logger)
    {
        ApplicationManager = applicationManager;
    }

    public override async Task AddRedirectDomainAsync(string clientId, string domainName)
    {
        var application = await ApplicationManager.FindByClientIdAsync(clientId);
        var descriptor = new AbpApplicationDescriptor();
        await ApplicationManager.PopulateAsync(descriptor, application);
        var redirectUrl = $"https://{domainName}/";
        if (!descriptor.RedirectUris.Any(u => u.Authority.Equals(redirectUrl, StringComparison.OrdinalIgnoreCase)))
        {
            redirectUrl += "signin-oidc";
            descriptor.RedirectUris.Add(new Uri(redirectUrl, UriKind.Absolute));
        }
        if (!descriptor.PostLogoutRedirectUris.Any(u => u.Authority.Equals(redirectUrl, StringComparison.OrdinalIgnoreCase)))
        {
            redirectUrl += "signout-callback-oidc";
            descriptor.PostLogoutRedirectUris.Add(new Uri(redirectUrl, UriKind.Absolute));
        }

        await ApplicationManager.UpdateAsync(application,descriptor);
    }

    public override async Task RemoveRedirectDomainAsync(string clientId, string domainName)
    {
        var application = await ApplicationManager.FindByClientIdAsync(clientId);
        var descriptor = new AbpApplicationDescriptor();
        await ApplicationManager.PopulateAsync(descriptor, application);
        descriptor.RedirectUris.RemoveAll(u => u.Authority.Equals(domainName, StringComparison.OrdinalIgnoreCase));
        descriptor.PostLogoutRedirectUris.RemoveAll(u => u.Authority.Equals(domainName, StringComparison.OrdinalIgnoreCase));

        await ApplicationManager.UpdateAsync(application, descriptor);
    }
}
