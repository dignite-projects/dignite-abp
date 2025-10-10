using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Dignite.Abp.Regionalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.RequestLocalization;
using Microsoft.Extensions.DependencyInjection;

namespace Dignite.Abp.AspNetCore.Mvc.Regionalization.Routing;

public class RegionalizationRouteDataRequestCultureProvider : RouteDataRequestCultureProvider
{
    /// <summary>
    /// The key that contains the culture name.
    /// Defaults to "culture".
    /// </summary>
    public static string RegionalizationRouteDataStringKey { get; set; } = "culture";

    /// <inheritdoc />
    public override async Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
    {
        var providerResultCulture = await base.DetermineProviderCultureResult(httpContext);
        string culture, uiCulture;
        if (providerResultCulture == NullProviderCultureResult.Result)
        {
            if (ShouldProcessCulture(httpContext))
            {
                /*
                 如果在切换语言后, 发现.AspNetCore.Culture这个cookie的值会在切换后语言和默认语言之间来回切换,
                这可能是因为CmsApplicationBuilderExtensions.UseCmsEndpoints方法中{*path:regex(^(?!swagger/|abp/|account/|libs/|.well-known/).*)}还缺少一些未知的前缀,
                因此,请通过断点调式来查看请求的路径, 以确定是否需要添加到正则表达式中.
                 */
                /* 2025-10-10 注释掉, 将这里的逻辑取消，不再使用默认文化, 而是直接返回NullProviderCultureResult.Result
                 * 而是交给更上层的开发者处理.
                 * 否则会导致在多语言站点中, 如果用户访问了不带语言前缀的URL, 会被强制重定向到默认语言, 这不是我们想要的.
                 * 我们希望用户访问不带语言前缀的URL时, 能够看到当前浏览器的首选语言(如果支持的话), 而不是默认语言.
                 * 
                 * 另外, 这里也不再设置CultureCookie, 让开发者来处理.
                 *
                var regionalizationRouteManager = httpContext.RequestServices.GetRequiredService<IRegionalizationRouteManager>();
                if (regionalizationRouteManager.TryMatchUrl(httpContext, out string? routePattern))
                {
                    var regionalizationProvider = httpContext.RequestServices.GetRequiredService<IRegionalizationProvider>();
                    var regionalization = await regionalizationProvider.GetRegionalizationAsync();
                    culture = regionalization.DefaultCulture.Name;
                    uiCulture = regionalization.DefaultCulture.CultureTypes.HasFlag(CultureTypes.NeutralCultures) ?
                        regionalization.DefaultCulture.Name :
                        regionalization.DefaultCulture.Parent.Name;
                }
                else
                {
                    // No values specified for either so no match
                    return NullProviderCultureResult.Result;
                }
                */
                // No values specified for either so no match
                return NullProviderCultureResult.Result;
            }
            else
            {
                // No values specified for either so no match
                return NullProviderCultureResult.Result;
            }
        }
        else
        {
            var firstStringSegmentCulture = providerResultCulture.Cultures.First().Value;
            var firstStringSegmentUiCulture = providerResultCulture.UICultures.First().Value;
            culture = firstStringSegmentCulture;
            uiCulture = firstStringSegmentUiCulture;
        }


        AbpRequestCultureCookieHelper.SetCultureCookie(
            httpContext,
            new RequestCulture(culture, uiCulture)
        );

        return new ProviderCultureResult(culture, uiCulture);
    }

    private bool ShouldProcessCulture(HttpContext httpContext)
    {
        var endpoint = httpContext.GetEndpoint();
        if (endpoint == null)
        {
            return false;
        }

        // 检查是否为 Razor Page
        var pageActionDescriptor = endpoint.Metadata.GetMetadata<CompiledPageActionDescriptor>();
        if (pageActionDescriptor != null)
        {
            return typeof(IRegionalizationRouteable).IsAssignableFrom(pageActionDescriptor.DeclaredModelTypeInfo);
        }

        // 检查是否为 MVC Controller
        var controllerActionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
        if (controllerActionDescriptor != null)
        {
            var controllerType = controllerActionDescriptor.ControllerTypeInfo;
            return typeof(IRegionalizationRouteable).IsAssignableFrom(controllerType);
        }

        return false;
    }
}
