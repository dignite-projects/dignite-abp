using Microsoft.Extensions.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;
using Dignite.Abp.LocaleManagement.Host.Localization;

namespace Dignite.Abp.LocaleManagement.Host;

[Dependency(ReplaceServices = true)]
public class LocaleManagementHostBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<LocaleManagementHostResource> _localizer;

    public LocaleManagementHostBrandingProvider(IStringLocalizer<LocaleManagementHostResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
