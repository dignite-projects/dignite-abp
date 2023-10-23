using Microsoft.AspNetCore.Http;

namespace Dignite.Abp.Wechat.OfficialAccount.WebApp;

/// <summary>
/// 授权验证的上下文信息
/// </summary>
public class GrantValidationContext
{
    public HttpContext HttpContext { get; }

    public OfficialAccountUserAccessToken AccessTokenResult { get; }

    public OfficialAccountUserInfo WechatUser { get; }

    public GrantValidationContext(HttpContext httpContext, OfficialAccountUserAccessToken accessTokenResult, OfficialAccountUserInfo wechatUser = null)
    {
        HttpContext = httpContext;
        WechatUser = wechatUser;
        AccessTokenResult = accessTokenResult;
    }
}
