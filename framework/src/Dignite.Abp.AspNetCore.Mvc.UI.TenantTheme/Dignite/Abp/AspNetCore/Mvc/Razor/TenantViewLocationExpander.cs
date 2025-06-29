﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Razor;
using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.AspNetCore.Mvc.Razor;
public class TenantViewLocationExpander : IViewLocationExpander
{
    private const string _cultureKey = "___culture";
    private const string _tenancyNameKey = "___tenantName";
    private const string _themeNameKey = "___themeName";
    private const string _webComponentPathKey = "___webComponentPath";

    private readonly Lazy<ICurrentTenant> _currentTenantLazy;
    private readonly Lazy<IThemeSelector> _themeSelectorLazy;

    public TenantViewLocationExpander(
        Lazy<ICurrentTenant> currentTenantLazy,
        Lazy<IThemeSelector> themeSelectorLazy)
    {
        _currentTenantLazy = currentTenantLazy;
        _themeSelectorLazy = themeSelectorLazy;
    }

    public void PopulateValues([NotNull] ViewLocationExpanderContext context)
    {
        // Using CurrentCulture so it loads the locale specific resources for the views.
        if (context.Values.GetOrDefault(_cultureKey) == null)
        {
            if (!string.IsNullOrEmpty(CultureInfo.CurrentCulture.Name))
                context.Values[_cultureKey] = CultureInfo.CurrentCulture.Name;
        }

        // Using CurrentTenant so it loads the tenant specific resources for the views.
        if (context.Values.GetOrDefault(_tenancyNameKey) == null)
        {
            if (_currentTenantLazy.Value.IsAvailable)
            {
                var tenantName = _currentTenantLazy.Value.Name;
                context.Values[_tenancyNameKey] = tenantName;
            }
        }

        // Using CurrentTheme so it loads the theme specific resources for the views.
        if (context.Values.GetOrDefault(_themeNameKey) == null)
        {
            var currentThemeName = _themeSelectorLazy.Value.GetCurrentThemeInfo().Name;
            context.Values[_themeNameKey] = currentThemeName;
        }

        // If viewname is a WebComponent then add the WebComponent path
        /* Regex result
         * If successful,
         * Group 0 = FullMatch (ex "Components/MyComponent/Default")
         * Group 1 = Components (ex "Components")
         * Group 2 = Component Name (ex "MyComponent")
         * Group 3 = View Name (ex "Default")
         * */
        var defaultComponentDetector = new Regex(@"^((?:[Cc]omponents))+\/+([\w\.\/]+)\/+(.*)");
        var defaultComponentMatch = defaultComponentDetector.Match(context.ViewName);

        if (defaultComponentMatch.Success)
        {
            // Will render Components/ComponentName as the new view name
            context.Values[_webComponentPathKey] =
                string.Format(
                    "{0}/{1}/{2}",
                    defaultComponentMatch.Groups[1].Value,
                    defaultComponentMatch.Groups[2].Value,
                    defaultComponentMatch.Groups[3].Value
                    );
        }
    }

    public IEnumerable<string> ExpandViewLocations([NotNull] ViewLocationExpanderContext context, [NotNull] IEnumerable<string> viewLocations)
    {
        var _viewLocations = GetViewLocations(context);
        var tenantName = context.Values.GetOrDefault(_tenancyNameKey);
        var currentThemeName = context.Values.GetOrDefault(_themeNameKey);

        if (!string.IsNullOrEmpty(tenantName))
        {
            var tenantViewLocations = new List<string>();
            foreach (var viewLocation in _viewLocations)
            {
                tenantViewLocations.Add("/Tenants/" + tenantName + "/Themes/" + currentThemeName + viewLocation);
                tenantViewLocations.Add("/Tenants/" + tenantName + viewLocation);
                tenantViewLocations.Add("/Themes/" + currentThemeName + viewLocation);
            }

            tenantViewLocations = tenantViewLocations.Concat(_viewLocations).ToList();
            viewLocations = tenantViewLocations.Concat(viewLocations);
        }
        else
        {
            var hostViewLocations = new List<string>();
            foreach (var viewLocation in _viewLocations)
            {
                hostViewLocations.Add("/Themes/" + currentThemeName + viewLocation);
            }

            hostViewLocations = hostViewLocations.Concat(_viewLocations).ToList();
            viewLocations = hostViewLocations.Concat(viewLocations);
        }
        return viewLocations;
    }

    public IList<string> GetViewLocations([NotNull] ViewLocationExpanderContext context)
    {
        var culture = "." + context.Values.GetOrDefault(_cultureKey);
        var webComponentPath = context.Values.GetOrDefault(_webComponentPathKey);
        IList<string> _viewLocations;

        //
        if (!string.IsNullOrEmpty(webComponentPath))
        {
            _viewLocations = new string[] {
                "/"+webComponentPath + culture + RazorViewEngine.ViewExtension,
                "/"+webComponentPath + RazorViewEngine.ViewExtension
            };
        }
        else if (!string.IsNullOrEmpty(context.AreaName))
        {
            /* Parameters:
             * {2} - Area Name
             * {1} - Controller Name
             * {0} - View Name
             */
            _viewLocations = new string[] {
                "/Areas/{2}/Views/{1}/{0}" + culture + RazorViewEngine.ViewExtension,
                "/Areas/{1}/Views/Shared/{0}" + culture + RazorViewEngine.ViewExtension,
                "/Areas/{2}/Views/{1}/{0}" + RazorViewEngine.ViewExtension,
                "/Areas/{1}/Views/Shared/{0}" + RazorViewEngine.ViewExtension
            };
        }
        else
        {
            /* Parameters:
             * {1} - Controller Name
             * {0} - View Name
             */
            _viewLocations = new string[] {
                "/Views/{1}/{0}" + culture + RazorViewEngine.ViewExtension,
                "/Views/Shared/{0}" + culture + RazorViewEngine.ViewExtension,
                "/Views/{1}/{0}" + RazorViewEngine.ViewExtension,
                "/Views/Shared/{0}" + RazorViewEngine.ViewExtension
            };
        }

        return _viewLocations;
    }
}