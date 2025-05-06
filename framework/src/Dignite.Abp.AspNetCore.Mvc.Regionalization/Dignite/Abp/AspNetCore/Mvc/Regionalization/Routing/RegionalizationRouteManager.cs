using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.Caching.Memory;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.AspNetCore.Mvc.Regionalization.Routing;

public class RegionalizationRouteManager : IRegionalizationRouteManager, ISingletonDependency
{
    private readonly EndpointDataSource _endpointDataSource;
    private readonly IMemoryCache _EndpointCache;

    public RegionalizationRouteManager(EndpointDataSource endpointDataSource, IMemoryCache endpointCache)
    {
        _endpointDataSource = endpointDataSource;
        _EndpointCache = endpointCache;
    }

    /// <summary>
    /// 判断当前页面Url是否匹配带有Culture路由参数的路由
    /// </summary>
    /// <returns></returns>
    public virtual bool TryMatchUrl(HttpContext httpContext, out string? routePattern)
    {
        routePattern = null;
        //获取所有包含culture路由参数的路由
        //这里创建了一个新list对象，确保对list的任何操作不影响GetAllEndpoints方法内部的缓存数据
        var regionalizationEndpoints = GetRegionalizationEndpoints();

        //
        var regionCultureName = httpContext.GetRouteValue(RegionalizationRouteDataRequestCultureProvider.RegionalizationRouteDataStringKey)?.ToString();

        //
        foreach (var endpoint in regionalizationEndpoints)
        {
            var routeEndpoint = (RouteEndpoint)endpoint;
            var routePatternRawText = routeEndpoint.RoutePattern.RawText;
            var requestPath = httpContext.Request.Path.Value;

            //如果当前请求的页面路径(requestPath)中不包含 region 路由参数
            //则根据本次循环的routePattern格式，向当前请求的页面路径(requestPath)中添加一个默认的region路由参数
            if (regionCultureName.IsNullOrEmpty())
            {
                var cultureSegment = $"{{{RegionalizationRouteDataRequestCultureProvider.RegionalizationRouteDataStringKey}:{RegionalizationRouteConstraint.ConstraintName}}}";
                var matchingCulture = "en";
                if (routePatternRawText.StartsWith(cultureSegment))
                {
                    requestPath = matchingCulture + requestPath;
                }
                else if (routePatternRawText.EndsWith(cultureSegment))
                {
                    requestPath = requestPath.EnsureEndsWith('/') + matchingCulture;
                }
                else
                {
                    continue;
                }
            }


            // 添加自定义约束处理器
            var routeTemplate = new RouteTemplate(routeEndpoint.RoutePattern);
            var defaults = new RouteValueDictionary(); // 定义默认值
            var matcher = new TemplateMatcher(routeTemplate, defaults);
            var values = new RouteValueDictionary();


            if (matcher.TryMatch(requestPath.EnsureStartsWith('/'), values))
            {
                // 解析 URL 段
                var urlSegments = requestPath.Trim('/').Split('/');
                if (urlSegments.Length > 0)
                {
                    // 验证文化约束
                    var constraint = new RegionalizationRouteConstraint();
                    if (constraint.Match(httpContext, null, RegionalizationRouteDataRequestCultureProvider.RegionalizationRouteDataStringKey, values, RouteDirection.IncomingRequest))
                    {
                        routePattern = routePatternRawText;
                        return true;
                    }
                }
            }
        }

        return false;
    }

    protected virtual List<Endpoint> GetRegionalizationEndpoints()
    {
        var cacheKey = "AbpContainRegionalizationEndpoints";
        var cacheValue = _EndpointCache.GetOrCreate(cacheKey, factory =>
        {
            return CreateEndpointsList();
        });
        return cacheValue;
    }

    private List<Endpoint> CreateEndpointsList()
    {
        var endpoints = new List<Endpoint>();

        // 添加常规路由端点
        foreach (var endpoint in _endpointDataSource.Endpoints)
        {
            var routeEndpoint = (RouteEndpoint)endpoint;
            var routePattern = routeEndpoint.RoutePattern;

            // 检查是否包含文化约束
            var hasCultureConstraint = routePattern.Parameters
                .Any(p => p.Name == RegionalizationRouteDataRequestCultureProvider.RegionalizationRouteDataStringKey &&
                         p.ParameterPolicies.Any(policy => policy.Content == RegionalizationRouteConstraint.ConstraintName)
                    );

            if (hasCultureConstraint)
            {
                if (!endpoints.Any(ep => ((RouteEndpoint)ep).RoutePattern.RawText == routeEndpoint.RoutePattern.RawText))
                {
                    endpoints.Add(endpoint);
                }
            }
        }

        return endpoints
            .OrderBy(ep => ((RouteEndpoint)ep).Order)
            .ToList();
    }
}
