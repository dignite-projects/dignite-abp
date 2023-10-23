using System.Text.Json.Serialization;

namespace Dignite.Abp.Wechat.OfficialAccount.Messages;

/// <summary>
/// 发送模板消息后的结果
/// </summary>
public class SendTemplateMessageResult : WechatResult
{
    /// <summary>
    /// msgid
    /// </summary>
    [JsonPropertyName("msgid")]
    public long MessageId { get; set; }
}
