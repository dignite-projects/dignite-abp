using System.Text.Json.Serialization;

namespace Dignite.Abp.Wechat.OfficialAccount.Messages;

public class TemplateMessageMiniProgram
{
    /// <summary>
    /// 小程序AppId（必填）
    /// </summary>
    [JsonPropertyName("appid")]
    public string AppId { get; set; }

    /// <summary>
    /// 路径，如：index?foo=bar
    /// </summary>
    [JsonPropertyName("pagepath")]
    public string PagePath { get; set; }
}
