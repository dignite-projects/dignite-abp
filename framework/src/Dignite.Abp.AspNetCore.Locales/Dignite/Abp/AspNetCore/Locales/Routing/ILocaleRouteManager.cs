using Microsoft.AspNetCore.Http;

namespace Dignite.Abp.AspNetCore.Locales.Routing;

/// <summary>
/// <see cref="ILocaleRouteManager"/> is an interface for managing localized routes.
/// </summary>
public interface ILocaleRouteManager
{
    /// <summary>
    /// The request context url tries to match the localed route
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="routePattern"></param>
    /// <returns>
    /// returns the route pattern if it succeeds.
    /// </returns>
    bool TryMatchUrl(HttpContext httpContext, out string? routePattern);
}
