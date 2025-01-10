using Microsoft.AspNetCore.Http;

namespace Dignite.Abp.AspNetCore.Mvc.Regionalization.Routing;
public interface IRegionalizationRouteManager
{
    /// <summary>
    /// The request context url tries to match the regionalized route
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="routePattern"></param>
    /// <returns>
    /// returns the route pattern if it succeeds.
    /// </returns>
    bool TryMatchUrl(HttpContext httpContext, out string? routePattern);
}
