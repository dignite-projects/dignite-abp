using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure;


[ThemeName(Name)]
public class PureTheme : ITheme, ITransientDependency
{
    public const string Name = "Pure";

    private readonly Lazy<IWebHostEnvironment> _hostingEnvironmentLazy;
    private readonly Lazy<ICurrentTenant> _currentTenantLazy;
    public PureTheme(
        Lazy<IWebHostEnvironment> hostingEnvironmentLazy,
        Lazy<ICurrentTenant> currentTenantLazy)
    {
        _hostingEnvironmentLazy = hostingEnvironmentLazy;
        _currentTenantLazy = currentTenantLazy;
    }

    public virtual string GetLayout(string name, bool fallbackToDefault = false)
    {
        return fallbackToDefault ? GetLayoutFilePath(name) : null;
    }

    protected virtual string GetLayoutFilePath(string name)
    {
        var hostingEnvironment = _hostingEnvironmentLazy.Value;
        var currentTenant = _currentTenantLazy.Value;
        var layout = $"~/Themes/Pure/Layouts/{name}.cshtml";

        if (currentTenant.Id.HasValue)
        {
            var tenantLayout = $"/Tenants/{currentTenant.Name}/Themes/Pure/Layouts/{name}.cshtml";
            var tenantLayoutPath = hostingEnvironment.ContentRootPath + tenantLayout;
            if (File.Exists(tenantLayoutPath))
            {
                return "~" + tenantLayout;
            }
        }

        return layout;
    }
}
