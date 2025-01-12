using Microsoft.Extensions.Localization;
using Dignite.Abp.MultiTenancyDomains.Host.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Dignite.Abp.MultiTenancyDomains.Host;

[Dependency(ReplaceServices = true)]
public class MultiTenancyDomainsHostBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<MultiTenancyDomainsHostResource> _localizer;

    public MultiTenancyDomainsHostBrandingProvider(IStringLocalizer<MultiTenancyDomainsHostResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
