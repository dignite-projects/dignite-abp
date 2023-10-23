using System.Text.Json.Serialization;

namespace Dignite.Abp.Wechat.OfficialAccount;

public class GlobalAccessToken : WechatResult
{
    /// <summary>
    /// 获取到的凭证
    /// </summary>
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    /// <summary>
    /// 凭证有效时间，单位：秒。目前是7200秒之内的值。
    /// </summary>
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }
}
