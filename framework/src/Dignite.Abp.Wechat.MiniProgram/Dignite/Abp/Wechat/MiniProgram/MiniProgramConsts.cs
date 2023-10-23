namespace Dignite.Abp.Wechat.MiniProgram;

public class MiniProgramConsts
{
    public const string HttpClientName = "DigniteAbpWechatMiniProgramHttpClient";
    /// <summary>
    /// 微信公众号、小程序联合验证时用的常量
    /// </summary>
    public const string UnionAuthenticationScheme = "wechat_union";

    /// <summary>
    /// 用于外部登陆时的LoginProvider名称
    /// </summary>
    public const string AuthenticationScheme = "wechat_miniprogram";


    /// <summary>
    /// 登陆地址
    /// </summary>
    public const string SignInPath = "/wechat/miniprogram/signin";
}
