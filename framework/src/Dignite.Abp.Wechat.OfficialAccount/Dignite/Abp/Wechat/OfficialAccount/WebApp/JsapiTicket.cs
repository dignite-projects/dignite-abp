
using System.Text.Json.Serialization;

namespace Dignite.Abp.Wechat.OfficialAccount.WebApp;

public class JsapiTicket : WechatResult
{
    /// <summary>
    /// 获取到的凭证
    /// </summary>
    [JsonPropertyName("ticket")]
    public string Ticket { get; set; }
    /// <summary>
    /// 凭证有效时间，单位：秒。目前是7200秒之内的值。
    /// </summary>
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }
}
