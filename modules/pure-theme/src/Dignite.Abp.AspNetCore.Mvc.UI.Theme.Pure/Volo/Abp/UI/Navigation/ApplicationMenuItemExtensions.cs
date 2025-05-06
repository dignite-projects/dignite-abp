using Dignite.Abp.UI.Navigation;

namespace Volo.Abp.UI.Navigation;
public static class ApplicationMenuItemExtensions
{
    public static ApplicationMenuItem SetSupportsLocalePrefix(this ApplicationMenuItem menuItem, bool enabled)
    {
        return menuItem.WithCustomData(ApplicationMenuItemConsts.SupportsLocalePrefixKeyName, enabled);
    }

    public static bool IsSupportsLocalePrefix(this ApplicationMenuItem menuItem)
    {
        if (menuItem.CustomData.TryGetValue(ApplicationMenuItemConsts.SupportsLocalePrefixKeyName, out object isSupportsLocalization))
        {
            if (isSupportsLocalization is bool isSupportsLocalizationBool)
            {
                return isSupportsLocalizationBool;
            }
            if (isSupportsLocalization is string isSupportsLocalizationString && bool.TryParse(isSupportsLocalizationString, out var result))
            {
                return result;
            }
        }

        return false;
    }
}
