using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.AspNetCore.Mvc.UI.TenantTheme;

/// <summary>
/// 
/// </summary>
public abstract class TenantThemeBase : ITheme
{
    protected readonly ICurrentTenant CurrentTenant;
    protected readonly IThemeSelector ThemeSelector;

    public TenantThemeBase( ICurrentTenant currentTenant, IThemeSelector themeSelector)
    {
        CurrentTenant = currentTenant;
        ThemeSelector = themeSelector;
    }

    public virtual string GetLayout(string name, bool fallbackToDefault = true)
    {
        return GetLayoutFilePath(name);
    }

    protected virtual string GetLayoutFilePath(string name)
    {
        var currentThemeName = ThemeSelector.GetCurrentThemeInfo().Name;
        var layout = $"~/Themes/{currentThemeName}/Layouts/{name}.cshtml";

        if (CurrentTenant.IsAvailable)
        {
            return $"~/Tenants/{CurrentTenant.Name}/Themes/{currentThemeName}/Layouts/{name}.cshtml";
        }

        return layout;
    }
}