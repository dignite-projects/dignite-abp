using Volo.Abp.AspNetCore.Mvc.UI.Theming;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure;
public static class PureThemeExtensions
{
    public static string GetApplicationLayout(this ITheme theme, string layoutName, bool fallbackToDefault = true)
    {
        return theme.GetLayout($"{StandardLayouts.Application}/{layoutName}", fallbackToDefault);
    }
}
