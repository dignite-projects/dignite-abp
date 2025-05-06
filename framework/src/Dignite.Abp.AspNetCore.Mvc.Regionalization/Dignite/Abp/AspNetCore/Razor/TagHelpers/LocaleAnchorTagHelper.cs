using System;
using Dignite.Abp.AspNetCore.Mvc.Regionalization.Routing;
using Dignite.Abp.Regionalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Threading;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.TagHelpers;


[HtmlTargetElement("a", Attributes = "[use-locale-prefix]")]
public class LocaleAnchorTagHelper : TagHelper
{
    /// <summary>
    /// if or not the link address is prefixed with a localized prefix, 
    /// if true, a localized prefix will be added to the link address
    /// </summary>
    /// <remarks>
    /// The value of UseLocalePrefix is only valid if the link address is an absolute path.
    /// </remarks>
    public bool UseLocalePrefix { get; set; } = false;


    [ViewContext, HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var urlHelper = ViewContext.GetUrlHelper();
        output.Attributes.TryGetAttribute("href", out var hrefAttribute);

        if (!UseLocalePrefix || hrefAttribute == null || hrefAttribute.Value.ToString().StartsWith('#'))
        {
            return;
        }

        var url = hrefAttribute.Value.ToString();
        if (url.StartsWith('#') || !urlHelper.IsLocalUrl(url))
        {
            return;
        }
        if (!url.StartsWith('~') && !url.StartsWith('/'))
        {
            return;
        }

        var culture = ViewContext.HttpContext.GetRouteValue(RegionalizationRouteDataRequestCultureProvider.RegionalizationRouteDataStringKey)?.ToString();

        if (string.IsNullOrEmpty(culture))
        {
            if (ViewContext.HttpContext.Request.Cookies.TryGetValue(
                CookieRequestCultureProvider.DefaultCookieName,
                out var cookieValue))
            {
                culture = CookieRequestCultureProvider.ParseCookieValue(cookieValue).Cultures[0].Value;
            }
        }

        if (!string.IsNullOrEmpty(culture))
        {
            var regionalizationProvider = ViewContext.HttpContext.RequestServices.GetRequiredService<IRegionalizationProvider>();
            var regionalization = AsyncHelper.RunSync(regionalizationProvider.GetRegionalizationAsync);
            var defaultCulture = regionalization.DefaultCulture.Name;
            if (!culture.Equals(defaultCulture, StringComparison.OrdinalIgnoreCase))
            {
                url = ("~/" + culture).EnsureEndsWith('/') + url.RemovePreFix("~").RemovePreFix("/");
            }
        }

        output.Attributes.SetAttribute("href", urlHelper.Content(url));
    }
}
