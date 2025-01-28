using Dignite.Abp.AspNetCore.Mvc.Regionalization.Routing;
using Dignite.Cms.Public.Entries;
using Dignite.Cms.Public.Sections;
using Dignite.Cms.Public.Web.Routing;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System;
using System.Globalization;

namespace Dignite.Cms.Public.Web.TagHelpers
{
    /// <summary>
    /// Tag helper for linking to entry
    /// </summary>
    [HtmlTargetElement("a", Attributes = "[entry],[section]")]
    public class LinkToEntryTagHelper : TagHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public EntryDto Entry { get; set; }
        public SectionDto Section { get; set; }


        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var entryUrl = GetEntryUrl(Entry, Section);
            output.Attributes.SetAttribute("href", entryUrl);
        }

        /// <summary>
        /// set entry url
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="section"></param>
        private string GetEntryUrl(EntryDto entry, SectionDto section)
        {
            var culture = ViewContext.HttpContext.GetRouteValue(RegionalizationRouteDataRequestCultureProvider.RegionalizationRouteDataStringKey)?.ToString();
            var currentCulture = CultureInfo.CurrentUICulture.Name;
            var url = entry.GetUrl(section);

            if (culture.IsNullOrEmpty())
            {
                url = url.RemovePreFix(StringComparison.OrdinalIgnoreCase, "/" + currentCulture + "/").EnsureStartsWith('/');
            }

            return url;
        }
    }
}
