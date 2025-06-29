﻿using System.Collections.Generic;
using Dignite.Abp.AspNetCore.Mvc.Regionalization.Routing;
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
        public static IApplicationBuilder UseCmsEndpoints(this IApplicationBuilder app, string[] ignoredEndpoints=null)
        {
            var integrateIgnoredEndpoints = new List<string> { "swagger/", "abp/", "account/", "libs/", ".well-known/", "error$" };
            if (ignoredEndpoints != null)
            {
                integrateIgnoredEndpoints.AddRange(ignoredEndpoints);
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: CmsEndpointNames.DefaultEndpointName,
                    pattern: "/",
                    defaults: new { controller = CmsController.ControllerName, action = nameof(CmsController.Default) });
                endpoints.MapControllerRoute(
                    name: CmsEndpointNames.RegionalizationEntryEndpointName,
                    pattern: "{culture:" + RegionalizationRouteConstraint.ConstraintName + "}/{*path}",
                    defaults: new { controller = CmsController.ControllerName, action = nameof(CmsController.CultureEntry) });
                endpoints.MapControllerRoute(
                    name: CmsEndpointNames.EntryEndpointName,
                    pattern: "{*path:regex(^(?!" + integrateIgnoredEndpoints.JoinAsString("|") + ").*)}",
                    defaults: new { controller = CmsController.ControllerName, action = nameof(CmsController.Entry) });
            });

            return app;
        }
    }
}
