using Dignite.Abp.AspNetCore.Mvc.Regionalization.Routing;
using Dignite.Cms.Public.Web.Controllers;
using Dignite.Cms.Public.Web.Routing;
using Microsoft.AspNetCore.Builder;

namespace Dignite.Cms.Public.Web.Builder
{
    public static class CmsApplicationBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCmsEndpoints(this IApplicationBuilder app)
        {
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
                    pattern: "{*path:regex(^(?!swagger/|abp/|account/|libs/|.well-known/|Error/).*)}", //TODO: Use an options to configure the regular expression for the route
                    defaults: new { controller = CmsController.ControllerName, action = nameof(CmsController.Entry) });
            });

            return app;
        }
    }
}
