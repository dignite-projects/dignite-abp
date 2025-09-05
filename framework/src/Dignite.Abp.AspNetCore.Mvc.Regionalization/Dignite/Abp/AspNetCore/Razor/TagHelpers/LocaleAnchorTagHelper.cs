using System;
using Dignite.Abp.Regionalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Threading;

namespace Dignite.Abp.AspNetCore.Mvc.UI.Theme.Pure.Themes.Pure.TagHelpers;


[HtmlTargetElement("a", Attributes = "[add-culture]")]
public class LocaleAnchorTagHelper : TagHelper
{
    /// <summary>
    /// if or not the link address is prefixed with a Culture, 
    /// if true, a Culture will be added to the link address
    /// </summary>
    /// <remarks>
    /// The value of AddCulture is only valid if the link address is an absolute path.
    /// </remarks>
    public bool AddCulture { get; set; } = false;


    [ViewContext, HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; }

    /// <summary>
    /// The order of execution for this TagHelper.
    /// Ensure priority over <see cref="UrlResolutionTagHelper"/> execution
    /// </summary>
    public override int Order => -2000; 

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (!AddCulture)
        {
            return;
        }

        if (context.AllAttributes.TryGetAttribute("href", out var hrefAttribute))
        {
            var url = hrefAttribute.Value.ToString();
            if (!url.StartsWith("~/") && !url.StartsWith('/'))
            {
                return;
            }
            var urlHelper = ViewContext.GetUrlHelper();
            if (url.StartsWith('#') || !urlHelper.IsLocalUrl(url))
            {
                return;
            }

            var cultureFeature = ViewContext.HttpContext?.Features.Get<IRequestCultureFeature>();
            var requestCultureName = cultureFeature?.RequestCulture.Culture.Name;

            if (!string.IsNullOrEmpty(requestCultureName))
            {
                var regionalizationProvider = ViewContext.HttpContext.RequestServices.GetRequiredService<IRegionalizationProvider>();
                var regionalization = AsyncHelper.RunSync(regionalizationProvider.GetRegionalizationAsync);
                var defaultCulture = regionalization.DefaultCulture.Name;
                if (regionalization.AvailableCultures.Count > 1)
                {
                    if (url.StartsWith("~/"))
                    {
                        url = requestCultureName.EnsureStartsWith('/').EnsureStartsWith('~') + url.RemovePreFix("~").RemovePostFix("/");
                    }
                    else
                    {
                        url = requestCultureName.EnsureStartsWith('/') + url.RemovePostFix("/");
                    }
                }
                output.Attributes.SetAttribute("href", url); //以上代码对href值处理完成后,其他的 TagHelper 将继续处理,例如 asp.net 内置的 UrlResolutionTagHelper
            }
        }
    }
}
