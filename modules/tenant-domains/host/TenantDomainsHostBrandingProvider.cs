using Microsoft.Extensions.Localization;
using Dignite.Abp.TenantDomains.Host.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Dignite.Abp.TenantDomains.Host;

[Dependency(ReplaceServices = true)]
public class TenantDomainsHostBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<TenantDomainsHostResource> _localizer;

    public TenantDomainsHostBrandingProvider(IStringLocalizer<TenantDomainsHostResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
