using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Dignite.Abp.Wechat.OfficialAccount.WebApp;

/// <summary>
/// 用于生成微信授权URL的中间件
/// </summary>
public class AuthorizationUrlMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebAppApiService _officAccountApiService;

    public AuthorizationUrlMiddleware(RequestDelegate next, IWebAppApiService officAccountApiService)
    {
        _next = next;
        _officAccountApiService = officAccountApiService;
    }

    public async Task Invoke(HttpContext context)
    {
        var request = context.Request;
        var response = context.Response;

        //若当前请求不是获取微信网页授权的地址，则跳过处理，直接执行后续中间件
        if (!OfficialAccountConsts.AuthorizationPath.Equals(request.Path, StringComparison.OrdinalIgnoreCase))
        {
            await _next(context);
            return;
        }

        var scope = (AuthencationScope)Enum.Parse(typeof(AuthencationScope), request.Query["scope"].ToString(), true);
        var returnUrl = request.Query["callbackUrl"].ToString();

        var authorizeUrl = await _officAccountApiService.GetAuthorizeUrlAsync(scope, returnUrl);
        response.Redirect(authorizeUrl, false);
    }
}
