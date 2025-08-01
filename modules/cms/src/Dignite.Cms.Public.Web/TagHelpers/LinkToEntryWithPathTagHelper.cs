using Dignite.Abp.AspNetCore.Locales.Routing;
using Dignite.Cms.Public.Web.Routing;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System;

namespace Dignite.Cms.Public.Web.TagHelpers
{
    /// <summary>
    /// Link to entry
    /// </summary>
    [Obsolete("Use dignite-abp\\framework\\src\\Dignite.Abp.AspNetCore.Locales\\Dignite\\Abp\\AspNetCore\\Razor\\TagHelpers\\CultureAnchorTagHelper.cs instead.")]
    [HtmlTargetElement("a", Attributes = "[entry-path]")]
    public class LinkToEntryWithPathTagHelper : TagHelper
    {
        /// <summary>
        /// Specify the path of the link to the entry
        /// </summary>
        public string EntryPath { get; set; }

        /// <summary>
        /// Specify Culture;
        /// If not specified, defaults to current Culture;
        /// </summary>
        public string Culture { get; set; }


        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = ViewContext.GetUrlHelper();

            if (urlHelper.IsLocalUrl(EntryPath))
            {
                if (Culture.IsNullOrEmpty())
                {
                    Culture = ViewContext.HttpContext.GetRouteValue(LocaleRouteDataRequestCultureProvider.LocaleRouteDataStringKey)?.ToString();
                }

                EntryPath = ("~/" + Culture).EnsureEndsWith('/') + EntryPath.RemovePreFix("~").RemovePreFix("/");
            }

            output.Attributes.SetAttribute("href", urlHelper.Content(EntryPath));
        }
    }
}
