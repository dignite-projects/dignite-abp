using Dignite.CmsKit.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.CmsKit.Settings;

public class CmsKitSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(
            new SettingDefinition(CmsKitSettings.BrandName,
                "My Dignite",
                displayName: L("DisplayName:CmsKit.BrandName"),
                isVisibleToClients: true
                ),
            new SettingDefinition(CmsKitSettings.BrandLogo,
                displayName: L("DisplayName:CmsKit.BrandLogo"),
                isVisibleToClients: true
                ),
            new SettingDefinition(CmsKitSettings.BrandLogoReverse,
                displayName: L("DisplayName:CmsKit.BrandLogoReverse"),
                isVisibleToClients: true
                ),
            new SettingDefinition(CmsKitSettings.BrandIcon,
                displayName: L("DisplayName:CmsKit.BrandIcon"),
                isVisibleToClients: true
                )
        );
    }
    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<DigniteCmsKitResource>(name);
    }
}
