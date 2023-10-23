using Dignite.Abp.Wechat.MiniProgram.Login;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Dignite.Abp.Wechat.MiniProgram;

/// <summary>
/// 注册微信小程序需要的公共服务
/// </summary>
public static class MiniProgramExtensions
{
    public static IServiceCollection AddWechatMiniProgram(this IServiceCollection services)
    {
        services.AddHttpClient(MiniProgramConsts.HttpClientName, client =>
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
    public static IApplicationBuilder UseWechatMiniProgram(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<MiniProgramGrantValidationMiddleware>();
    }

    /// <summary>
    /// 微信小程序授权登陆后，进入配置的<see cref="IGrantValidateHandler"/>
    /// </summary>
    /// <typeparam name="THandler"></typeparam>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddMiniProgramGrantValidationHandler<THandler>(this IServiceCollection services)
           where THandler : class, IGrantValidateHandler
    {
        return services.AddScoped<IGrantValidateHandler, THandler>();
    }
}
