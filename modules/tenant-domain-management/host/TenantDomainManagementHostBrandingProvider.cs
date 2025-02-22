using Microsoft.Extensions.Localization;
using Dignite.Abp.TenantDomainManagement.Host.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Dignite.Abp.TenantDomainManagement.Host;

[Dependency(ReplaceServices = true)]
public class TenantDomainManagementHostBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<TenantDomainManagementHostResource> _localizer;

    public TenantDomainManagementHostBrandingProvider(IStringLocalizer<TenantDomainManagementHostResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
