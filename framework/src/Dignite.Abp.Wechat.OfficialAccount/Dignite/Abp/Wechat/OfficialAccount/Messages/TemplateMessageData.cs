using System.Text.Json.Serialization;

namespace Dignite.Abp.Wechat.OfficialAccount.Messages;

/// <summary>
/// 模板消息的数据
/// </summary>
public class TemplateMessageData
{
    /// <summary>
    /// 项目值
    /// </summary>
    [JsonPropertyName("value")]
    public string Value { get; set; }
    /// <summary>
    /// 16进制颜色代码，如：#FF0000
    /// </summary>
    [JsonPropertyName("color")]
    public string Color { get; set; }

    /// <summary>
    /// TemplateDataItem 构造函数
    /// </summary>
    /// <param name="value">value</param>
    /// <param name="color">color</param>
    public TemplateMessageData(string value, string color = "#173177")
    {
        this.Value = value;
        this.Color = color;
    }
}
