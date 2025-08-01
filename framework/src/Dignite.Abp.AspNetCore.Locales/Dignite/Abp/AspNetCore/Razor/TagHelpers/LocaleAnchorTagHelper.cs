using System;
using Dignite.Abp.Locales;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Threading;

namespace Dignite.Abp.AspNetCore.Razor.TagHelpers;


[HtmlTargetElement("a", Attributes = "[add-locale]")]
public class LocaleAnchorTagHelper : TagHelper
{
    /// <summary>
    /// if or not the link address is prefixed with a Culture, 
    /// if true, a Culture will be added to the link address
    /// </summary>
    public bool AddLocale { get; set; } = false;


    [ViewContext, HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; }

    /// <summary>
    /// The order of execution for this TagHelper.
    /// Ensure priority over <see cref="UrlResolutionTagHelper"/> execution
    /// </summary>
    public override int Order => -2000; 

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (!AddLocale)
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
            var cultureName = cultureFeature?.RequestCulture.Culture.Name;

            if (!string.IsNullOrEmpty(cultureName))
            {
                var localeProvider = ViewContext.HttpContext.RequestServices.GetRequiredService<ILocaleProvider>();
                var locale = AsyncHelper.RunSync(localeProvider.GetLocaleAsync);
                var defaultCulture = locale.DefaultCulture.Name;
                if (!cultureName.Equals(defaultCulture, StringComparison.OrdinalIgnoreCase))
                {
                    if (url.StartsWith("~/"))
                    {
                        url = cultureName.EnsureStartsWith('/').EnsureStartsWith('~') + url.RemovePreFix("~").RemovePostFix("/");
                    }
                    else
                    {
                        url = cultureName.EnsureStartsWith('/') + url.RemovePostFix("/");
                    }
                }
                output.Attributes.SetAttribute("href", url); //以上代码对href值处理完成后,其他的 TagHelper 将继续处理,例如 asp.net 内置的 UrlResolutionTagHelper
            }
        }
    }
}
