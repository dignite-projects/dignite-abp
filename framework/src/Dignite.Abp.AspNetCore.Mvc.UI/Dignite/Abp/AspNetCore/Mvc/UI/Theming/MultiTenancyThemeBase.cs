using System.IO;
using Microsoft.AspNetCore.Hosting;
using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theming;

/// <summary>
/// 
/// </summary>
public abstract class MultiTenancyThemeBase : ITheme
{
    public IAbpLazyServiceProvider LazyServiceProvider { get; set; }
    protected IWebHostEnvironment HostingEnvironment => LazyServiceProvider.LazyGetRequiredService<IWebHostEnvironment>();
    protected ICurrentTenant CurrentTenant => LazyServiceProvider.LazyGetRequiredService<ICurrentTenant>();
    protected IThemeSelector ThemeSelector => LazyServiceProvider.LazyGetRequiredService<IThemeSelector>();



    public virtual string GetLayout(string name, bool fallbackToDefault = false)
    {
        return fallbackToDefault ? GetLayoutFilePath(name) : null;
    }

    protected virtual string GetLayoutFilePath(string name)
    {
        var currentThemeName = ThemeSelector.GetCurrentThemeInfo().Name;
        var layout = $"~/Themes/{currentThemeName}/Layouts/{name}.cshtml";

        if (CurrentTenant.Id.HasValue)
        {
            var tenantLayout = $"/Tenants/{CurrentTenant.Name}/Themes/{currentThemeName}/Layouts/{name}.cshtml";
            var tenantLayoutPath = HostingEnvironment.ContentRootPath + tenantLayout;
            if (File.Exists(tenantLayoutPath))
            {
                return "~" + tenantLayout;
            }
        }

        return layout;
    }
}