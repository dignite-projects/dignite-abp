using Dignite.Abp.AspNetCore.Locales.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.Components.Toolbar.LocaleSwitch;

public class LocaleSwitchViewComponentModel
{
    public LocaleSwitchViewComponentModel(string defaultCultureName, string currentCultureName, IReadOnlyList<CultureInfo> availableCultures, bool isMatchingRoute,string routePattern)
    {
        DefaultCultureName = defaultCultureName;
        CurrentCultureName = currentCultureName;
        AvailableCultures = availableCultures;
        IsMatchingRoute = isMatchingRoute;
        RoutePattern = routePattern;
    }

    public string DefaultCultureName { get; }

    public string CurrentCultureName { get; }

    public IReadOnlyList<CultureInfo> AvailableCultures { get; }

    public bool IsMatchingRoute { get; }

    public string RoutePattern { get; }

    public string InsertOrReplaceCultureParameter(HttpContext httpContext, string culture)
    {
        var url = httpContext.Request.Path.Value;

        if (!IsMatchingRoute)
            return url;

        // Parse route pattern segments
        var patternSegments = RoutePattern.TrimStart('/').Split('/');
        var urlSegments = url.Trim('/').Split('/');
        var cultureSegment = $"{{{LocaleRouteDataRequestCultureProvider.LocaleRouteDataStringKey}:{LocaleRouteConstraint.ConstraintName}}}";

        // Find culture parameter position in pattern
        int culturePosition = -1;
        for (int i = 0; i < patternSegments.Length; i++)
        {
            if (patternSegments[i].Replace(" ", "").Contains(cultureSegment, StringComparison.OrdinalIgnoreCase))
            {
                culturePosition = i;
                break;
            }
        }

        if (culturePosition == -1) return url; //

        // Insert culture parameter
        var newSegments = new List<string>(urlSegments);
        if (httpContext.GetRouteValue(LocaleRouteDataRequestCultureProvider.LocaleRouteDataStringKey)!=null)
        {
            if (culture.Equals(DefaultCultureName, StringComparison.OrdinalIgnoreCase))
            {
                newSegments.RemoveAt(culturePosition);
            }
            else
            {
                newSegments[culturePosition] = culture;
            }
        }
        else
        {
            newSegments.Insert(culturePosition, culture);
        }
        return "/" + string.Join("/", newSegments);
    }
}