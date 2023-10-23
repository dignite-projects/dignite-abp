using System.Text.Json.Serialization;

namespace Dignite.Abp.Wechat.MiniProgram.Login;

/// <summary>
/// 
/// </summary>
public class MiniProgramUserSession : WechatResult
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("OpenId")]
    public string OpenId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("session_key")]
    public string SessionKey { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("unionid")]
    public string UnionId { get; set; }
}
