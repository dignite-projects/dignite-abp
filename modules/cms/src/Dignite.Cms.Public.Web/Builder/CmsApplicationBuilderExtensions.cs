using System.Collections.Generic;
using Dignite.Abp.AspNetCore.Locales.Routing;
using Dignite.Cms.Public.Web.Controllers;
using Dignite.Cms.Public.Web.Routing;
using Microsoft.AspNetCore.Builder;

namespace Dignite.Cms.Public.Web.Builder
{
    public static class CmsApplicationBuilderExtensions
    {
        /// <summary>
        /// UseCmsEndpoints is used to register CMS endpoints in the application pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="ignoredEndpoints"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCmsEndpoints(this IApplicationBuilder app, bool shouldRegisterDefaultRoute=true, string[] excludedRoutePrefixes = null)
        {
            var predefinedExcludedPrefixes = new List<string> { "swagger/", "abp/", "account/", "libs/", ".well-known/", "error$" };
            if (excludedRoutePrefixes != null)
            {
                predefinedExcludedPrefixes.AddRange(excludedRoutePrefixes);
            }

            app.UseEndpoints(endpoints =>
            {
                if (shouldRegisterDefaultRoute)
                {
                    endpoints.MapControllerRoute(
                        name: CmsEndpointNames.DefaultEndpointName,
                        pattern: "/",
                        defaults: new { controller = CmsController.ControllerName, action = nameof(CmsController.Default) });
                }
                endpoints.MapControllerRoute(
                    name: CmsEndpointNames.LocaleEntryEndpointName,
                    pattern: "{culture:" + LocaleRouteConstraint.ConstraintName + "}/{*path}",
                    defaults: new { controller = CmsController.ControllerName, action = nameof(CmsController.CultureEntry) });
                endpoints.MapControllerRoute(
                    name: CmsEndpointNames.EntryEndpointName,
                    pattern: "{*path:regex(^(?!" + predefinedExcludedPrefixes.JoinAsString("|") + ").*)}",
                    defaults: new { controller = CmsController.ControllerName, action = nameof(CmsController.Entry) });
            });

            return app;
        }
    }
}
