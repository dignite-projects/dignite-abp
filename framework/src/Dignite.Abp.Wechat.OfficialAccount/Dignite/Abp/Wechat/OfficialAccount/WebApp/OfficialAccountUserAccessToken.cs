using System;
using System.Text.Json.Serialization;

namespace Dignite.Abp.Wechat.OfficialAccount.WebApp;


/// <summary>
/// Wechat User AccessToken
/// </summary>
[Serializable]
public class OfficialAccountUserAccessToken : WechatResult
{
    /// <summary>
    /// 接口调用凭证
    /// </summary>
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
    /// <summary>
    /// access_token接口调用凭证超时时间，单位（秒）
    /// </summary>
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }
    /// <summary>
    /// 用户刷新access_token
    /// </summary>
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }
    /// <summary>
    /// 授权用户唯一标识
    /// </summary>
    [JsonPropertyName("openid")]
    public string OpenId { get; set; }
    /// <summary>
    /// 用户授权的作用域，使用逗号（,）分隔
    /// </summary>
    [JsonPropertyName("scope")]
    public string Scope { get; set; }
    /// <summary>
    /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。详见：获取用户个人信息（UnionID机制）
    /// </summary>
    [JsonPropertyName("unionid")]
    public string UnionId { get; set; }

    /// <summary>
    /// <see cref="AccessToken"/> 过期时间
    /// </summary>
    internal DateTime AccessTokenExpireTime { get; set; }


    /// <summary>
    /// <see cref="RefreshToken"/> 过期时间
    /// </summary>
    internal DateTime RefreshTokenExpireTime { get; set; }
}
