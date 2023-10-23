using System.Text.Json.Serialization;

namespace Dignite.Abp.Wechat;

public class WechatResult
{
    [JsonPropertyName("errcode")]
    public int ErrorCode { get; set; }

    [JsonPropertyName("errmsg")]
    public string ErrorMessage { get; set; }
}
