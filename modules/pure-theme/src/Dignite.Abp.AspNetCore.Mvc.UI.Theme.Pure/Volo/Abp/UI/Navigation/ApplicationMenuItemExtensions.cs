using Dignite.Abp.UI.Navigation;

namespace Volo.Abp.UI.Navigation;
public static class ApplicationMenuItemExtensions
{
    public static ApplicationMenuItem SupportsLocale(this ApplicationMenuItem menuItem, bool enabled)
    {
        return menuItem.WithCustomData(ApplicationMenuItemConsts.SupportsLocaleKeyName, enabled);
    }

    public static bool IsSupportsLocale(this ApplicationMenuItem menuItem)
    {
        if (menuItem.CustomData.TryGetValue(ApplicationMenuItemConsts.SupportsLocaleKeyName, out object isSupportsLocalization))
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
