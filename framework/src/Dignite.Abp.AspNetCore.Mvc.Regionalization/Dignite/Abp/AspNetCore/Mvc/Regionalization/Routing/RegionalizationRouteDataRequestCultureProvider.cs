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
