namespace Dignite.Abp.Wechat.OfficialAccount;

public class OfficialAccountConsts
{
    public const string HttpClientName = "DigniteAbpWechatOfficialAccountHttpClient";
    /// <summary>
    /// 微信公众号、小程序联合验证时用的常量
    /// </summary>
    public const string UnionAuthenticationScheme = "wechat_union";


    /// <summary>
    /// 用于外部登陆时的LoginProvider名称
    /// </summary>
    public const string AuthenticationScheme = "wechat_webapp";


    /// <summary>
    /// 获取微信网页授权地址
    /// </summary>
    public const string AuthorizationPath = "/wechat/webapp/authorization";

    /// <summary>
    /// 登陆地址
    /// </summary>
    public const string SignInPath = "/wechat/webapp/signin";

    /// <summary>
    /// 获取jsapi加密签名的地址
    /// </summary>
    public const string JsapiSignaturePath = "/api/wechat/webapp/get-jsapi-signature";
}
