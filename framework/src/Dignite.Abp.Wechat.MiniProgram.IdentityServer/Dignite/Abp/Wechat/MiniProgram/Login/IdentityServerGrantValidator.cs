using System;
using System.Threading.Tasks;
using Dignite.Abp.Wechat.MiniProgram.IdentityServer;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Dignite.Abp.Wechat.MiniProgram.Login;

public class IdentityServerGrantValidator : IExtensionGrantValidator
{
    private readonly ILoginApiService _loginApiService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdentityServerGrantValidator(
        ILoginApiService loginApiService,
        IHttpContextAccessor httpContextAccessor
        )
    {
        _loginApiService = loginApiService;
        _httpContextAccessor = httpContextAccessor;
    }

    public string GrantType => IdentityServerConsts.WechatMiniProgramGrantType;

    public async Task ValidateAsync(ExtensionGrantValidationContext context)
    {
        try
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var handler = httpContext.RequestServices.GetService<IGrantValidateHandler>();


            if (handler == null)
                throw new Exception($"请实现{nameof(IGrantValidateHandler)}，并注册依赖！");

            //微信小程序登陆的code
            var code = context.Request.Raw["code"];
            var userInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<MiniProgramUserInfo>(context.Request.Raw["userInfo"]);
            var sessionResult = await _loginApiService.GetSessionTokenAsync(code);
            userInfo.OpenId = sessionResult?.OpenId;
            userInfo.UnionId = sessionResult?.UnionId;
            //Log.Information($"userInfo:{Newtonsoft.Json.JsonConvert.SerializeObject(userInfo)}");
            var grantValidationResult = await handler.ExcuteAsync(
                new GrantValidationContext(httpContext, sessionResult, userInfo)
                );

            if (grantValidationResult.ErrorCode == 0)
            {
                //授权通过返回
                context.Result = new IdentityServer4.Validation.GrantValidationResult
                (
                    subject: grantValidationResult.UserId,
                    authenticationMethod: GrantType,
                    claims: grantValidationResult.AddedClaims
                );
            }
            else
            {
                context.Result = new IdentityServer4.Validation.GrantValidationResult()
                {
                    IsError = true,
                    Error = "未绑定一个用户，请跳转到账号密码登陆页面！"
                };
            }
        }
        catch (Exception ex)
        {
            context.Result = new IdentityServer4.Validation.GrantValidationResult()
            {
                IsError = true,
                Error = ex.Message
            };
        }
    }
}