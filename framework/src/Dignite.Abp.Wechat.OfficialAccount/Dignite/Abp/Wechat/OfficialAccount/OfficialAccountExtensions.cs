using Dignite.Abp.Wechat.OfficialAccount.WebApp;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Dignite.Abp.Wechat.OfficialAccount;

/// <summary>
/// 注册微信公众号需要的公共服务
/// </summary>
public static class OfficialAccountExtensions
{
    public static IServiceCollection AddWechatOfficialAccount(this IServiceCollection services)
    {
        services.AddHttpClient(OfficialAccountConsts.HttpClientName, client =>
        {
            //这里可以统一对client进行配置
        });

        return services;
    }

    /// <summary>
    /// 向中间件管道注册微信小程序登陆中间件
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseWechatOfficialAccount(this IApplicationBuilder builder)
    {
        return builder
            .UseMiddleware<WebAppGrantValidationMiddleware>()
            .UseMiddleware<AuthorizationUrlMiddleware>()
            .UseMiddleware<JsapiSignatureMiddleware>();
    }

    /// <summary>
    /// 微信公众号网页授权登陆后，进入配置的<see cref="IGrantValidationHandler"/>
    /// </summary>
    /// <typeparam name="THandler"></typeparam>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddWebAppGrantValidationHandler<THandler>(this IServiceCollection services)
           where THandler : class, IGrantValidationHandler
    {
        return services.AddScoped<IGrantValidationHandler, THandler>();
    }
}
