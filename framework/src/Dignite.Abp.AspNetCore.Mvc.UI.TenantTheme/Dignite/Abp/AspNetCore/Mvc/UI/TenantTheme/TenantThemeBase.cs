using System.IO;
using Microsoft.AspNetCore.Hosting;
using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.AspNetCore.Mvc.UI.TenantTheme;

/// <summary>
/// 
/// </summary>
public abstract class TenantThemeBase : ITheme
{
    protected readonly IWebHostEnvironment HostingEnvironment;
    protected readonly ICurrentTenant CurrentTenant;
    protected readonly IThemeSelector ThemeSelector;

    public TenantThemeBase(IWebHostEnvironment hostingEnvironment, ICurrentTenant currentTenant, IThemeSelector themeSelector)
    {
        HostingEnvironment = hostingEnvironment;
        CurrentTenant = currentTenant;
        ThemeSelector = themeSelector;
    }

    public virtual string GetLayout(string name, bool fallbackToDefault = true)
    {
        return GetLayoutFilePath(name);
    }

    protected virtual string GetLayoutFilePath(string name, bool fallbackToDefault = true)
    {
        var currentThemeName = ThemeSelector.GetCurrentThemeInfo().Name;
        var layout = $"~/Themes/{currentThemeName}/Layouts/{name}.cshtml";

        if (CurrentTenant.IsAvailable)
        {
            var tenantLayout = $"/Tenants/{CurrentTenant.Name}/Themes/{currentThemeName}/Layouts/{name}.cshtml";
            var tenantLayoutPath = HostingEnvironment.ContentRootPath + tenantLayout;
            if (File.Exists(tenantLayoutPath))
            {
                return "~" + tenantLayout;
            }
            else
            {
                if (fallbackToDefault)
                {
                    return layout;
                }
                else
                {
                    return null;
                }
            }
        }

        return layout;
    }
}