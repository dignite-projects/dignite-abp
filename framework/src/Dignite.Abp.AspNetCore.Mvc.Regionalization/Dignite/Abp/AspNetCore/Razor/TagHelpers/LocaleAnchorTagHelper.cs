using System;
using System.Globalization;
using Dignite.Abp.AspNetCore.Mvc.Regionalization.Routing;
using Dignite.Abp.Regionalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Localization;
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

    public override int Order => -2000; // 设置较低的 Order，确保优先于 UrlResolutionTagHelper 执行

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (!UseLocalePrefix)
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

            // 获取当前请求的 Culture
            var cultureFeature = ViewContext.HttpContext?.Features.Get<IRequestCultureFeature>();
            var cultureName = cultureFeature?.RequestCulture.Culture.Name;

            if (!string.IsNullOrEmpty(cultureName))
            {
                var regionalizationProvider = ViewContext.HttpContext.RequestServices.GetRequiredService<IRegionalizationProvider>();
                var regionalization = AsyncHelper.RunSync(regionalizationProvider.GetRegionalizationAsync);
                var defaultCulture = regionalization.DefaultCulture.Name;
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
                output.Attributes.SetAttribute("href", url); //以上代码对href值处理完成后,交给其他的 TagHelper 处理,例如 asp.net 内置的 UrlResolutionTagHelper
            }
        }
    }
}
