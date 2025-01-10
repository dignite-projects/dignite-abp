using Microsoft.Extensions.Localization;
using Dignite.Abp.RegionalizationManagement.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Dignite.Abp.RegionalizationManagement;

[Dependency(ReplaceServices = true)]
public class RegionalizationManagementBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<RegionalizationManagementResource> _localizer;

    public RegionalizationManagementBrandingProvider(IStringLocalizer<RegionalizationManagementResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
