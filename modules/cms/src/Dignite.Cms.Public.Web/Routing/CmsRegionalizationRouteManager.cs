using Dignite.Abp.AspNetCore.Mvc.Regionalization.Routing;
using Dignite.Cms.Public.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.DependencyInjection;

namespace Dignite.Cms.Public.Web.Routing;

[Dependency(ReplaceServices = true)]
public class CmsRegionalizationRouteManager : RegionalizationRouteManager
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CmsRegionalizationRouteManager(IHttpContextAccessor httpContextAccessor, EndpointDataSource endpointDataSource, IMemoryCache endpointCache) : base(endpointDataSource, endpointCache)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override List<Endpoint> GetRegionalizationEndpoints()
    {
        var endpoints = base.GetRegionalizationEndpoints();
        HttpContext httpContext = _httpContextAccessor.HttpContext;
        ArgumentNullException.ThrowIfNull(httpContext);

        //当路由表中找不到匹配的路由时，最终将使用{culture:regionalization}/{*path}路由
        //造成任意页面URL都将匹配到一个路由，这种机制应该只适用于Cms Entry的页面
        //因此，如果当前页面不是Cms Entry时，则移除allEndpoints中{culture:regionalization}/{*path}的路由
        var routeData = httpContext.GetRouteData();
        if (!routeData.Values.TryGetValue("controller", out var controllerName) ||
            !string.Equals(controllerName.ToString(), CmsController.ControllerName, StringComparison.OrdinalIgnoreCase))
        {
            var cmsControllerType = typeof(CmsController);
            var cmsControllerModuleName = cmsControllerType.Module.Name.Substring(0, cmsControllerType.Module.Name.LastIndexOf('.')); //Value is (Dignite.Cms.Public.Web)

            //移除allEndpoints中{culture:regionalization}/{*path}的路由
            //即移除DisplayName为 Dignite.Cms.Public.Web.Controllers.CmsController.CultureEntry (Dignite.Cms.Public.Web) 的路由
            var epDisplayName = $"{cmsControllerType.FullName}.{nameof(CmsController.CultureEntry)} ({cmsControllerModuleName})";
            return endpoints.Where(ep => ep.DisplayName != epDisplayName)
                .ToList();
        }

        return endpoints;
    }
}
