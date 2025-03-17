using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Localization;
using Volo.Abp.Threading;

namespace Dignite.Abp.AspNetCore.Mvc.Regionalization.Routing;

/// <summary>
/// This is a constraint class for the route parameter Culture. 
/// This is used when constraining the Culture route parameter in the configuration route pattern using the code {culture:regionalization}.
/// </summary>
public class RegionalizationRouteConstraint : IRouteConstraint
{
    public const string ConstraintName = "regionalization";

    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        ArgumentNullException.ThrowIfNull(httpContext);

        if (!values.TryGetValue(routeKey, out var culture))
        {
            return false; // 如果路由键不存在，则返回不匹配
        }


        // 如果值为 null，则允许通过
        if (culture == null)
        {
            return true; // 或根据需求返回 false
        }
        else
        {
            var languageProvider = httpContext.RequestServices.GetRequiredService<ILanguageProvider>();
            var languages = AsyncHelper.RunSync(() => languageProvider.GetLanguagesAsync());

            Regex rgx = new Regex(@"^(" + languages.Select(l => l.CultureName).JoinAsString("|") + ")$", RegexOptions.IgnoreCase);
#pragma warning disable CS8604 // 引用类型参数可能为 null。
            if (rgx.IsMatch(culture.ToString()))
            {
                return true;
            }
#pragma warning restore CS8604 // 引用类型参数可能为 null。

            return false;
        }
    }
}
