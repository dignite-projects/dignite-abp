using Dignite.Abp.Regionalization.Resources;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.Regionalization;

public class RegionalizationSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(
            new SettingDefinition(
                RegionalizationSettingNames.DefaultCultureName,
                "en-us",
                L("DisplayName:Abp.Regionalization.DefaultCultureName"),
                L("Description:Abp.Regionalization.DefaultCultureName"),
                true
                )
        );

        context.Add(
            new SettingDefinition(
                RegionalizationSettingNames.AvailableCultureNames,
                "en-us,zh-cn,ja-jp",
                L("DisplayName:Abp.Regionalization.AvailableCultureNames"),
                L("Description:Abp.Regionalization.AvailableCultureNames"),
                true
                )
        );
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AbpRegionalizationResource>(name);
    }
}
