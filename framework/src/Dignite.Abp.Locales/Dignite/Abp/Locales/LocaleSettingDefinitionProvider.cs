using Dignite.Abp.Locales.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Dignite.Abp.Locales;

public class LocaleSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(
            new SettingDefinition(
                LocaleSettingNames.DefaultCultureName,
                "en-us",
                L("DisplayName:Abp.Locale.DefaultCultureName"),
                L("Description:Abp.Locale.DefaultCultureName"),
                true
                )
        );

        context.Add(
            new SettingDefinition(
                LocaleSettingNames.AvailableCultureNames,
                "en-us,zh-cn,ja-jp",
                L("DisplayName:Abp.Locale.AvailableCultureNames"),
                L("Description:Abp.Locale.AvailableCultureNames"),
                true
                )
        );
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AbpLocaleResource>(name);
    }
}
