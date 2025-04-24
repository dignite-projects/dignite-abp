using System;
using System.Collections.Generic;
using Dignite.Abp.AspNetCore.Mvc.Regionalization.Routing;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.TagHelpers;


[HtmlTargetElement("a")]
public class CultureAnchorTagHelper : TagHelper
{
    /// <summary>
    /// Specify Culture;
    /// If not specified, defaults to current Culture;
    /// </summary>
    [HtmlAttributeName("culture")]
    public string? Culture { get; set; }


    [ViewContext, HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var urlHelper = ViewContext.GetUrlHelper();
        output.Attributes.TryGetAttribute("href", out var hrefAttribute);

        if (hrefAttribute == null)
        {
            return;
        }

        var href = hrefAttribute.Value.ToString();

        if (Culture.IsNullOrEmpty() && !context.AllAttributes.ContainsName("culture"))
        {
            Culture = ViewContext.HttpContext.GetRouteValue(RegionalizationRouteDataRequestCultureProvider.RegionalizationRouteDataStringKey)?.ToString();
        }

        href = ("~/" + Culture).EnsureEndsWith('/') + href.RemovePreFix("~").RemovePreFix("/");

        output.Attributes.SetAttribute("href", urlHelper.Content(href));
    }
}
