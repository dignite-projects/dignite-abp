using Dignite.Abp.Wechat.OfficialAccount.IdentityServer;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Volo.Abp;

namespace Dignite.Abp.Wechat.OfficialAccount.WebApp;

/// <summary>
/// 微信公众号网页的用户授权验证；
/// </summary>
public class IdentityServerGrantValidator : IExtensionGrantValidator
{
    private readonly IWebAppApiService _apiService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdentityServerGrantValidator(
        IWebAppApiService apiService,
        IHttpContextAccessor httpContextAccessor
        )
    {
        _apiService = apiService;
        _httpContextAccessor = httpContextAccessor;
    }

    public string GrantType => IdentityServerConsts.WechatOfficialAccountGrantType;

    public async Task ValidateAsync(ExtensionGrantValidationContext context)
    {
        try
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var handler = httpContext.RequestServices.GetService<IGrantValidationHandler>();


            if (handler == null)
                throw new AbpException("No grant validate handler was registered! At least one handler must be registered to be able to use the grant validate.");


            //授权后拿到code和state
            var code = context.Request.Raw["code"];
            var state = context.Request.Raw["state"];

            //换取token
            var sessionResult = await _apiService.ExchangeAccessTokenAsync(code, state);
            var userInfo = await _apiService.GetUserInfoAsync(sessionResult);

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
                context.Result = new IdentityServer4.Validation.GrantValidationResult
                {
                    IsError = true,
                    Error = grantValidationResult.ErrorCode.ToString(),
                    ErrorDescription = grantValidationResult.ErrorMessage
                };
            }
        }
        catch (Exception ex)
        {
            context.Result = new IdentityServer4.Validation.GrantValidationResult()
            {
                IsError = true,
                ErrorDescription = ex.Message
            };
        }
    }
}