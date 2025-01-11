using Microsoft.Extensions.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;
using Dignite.Abp.RegionalizationManagement.Host.Localization;

namespace Dignite.Abp.RegionalizationManagement.Host;

[Dependency(ReplaceServices = true)]
public class RegionalizationManagementHostBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<RegionalizationManagementHostResource> _localizer;

    public RegionalizationManagementHostBrandingProvider(IStringLocalizer<RegionalizationManagementHostResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
