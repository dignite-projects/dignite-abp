namespace Dignite.Abp.Wechat.MiniProgram.Link;

public class QrCode : WechatResult
{
    public string ContentType { get; set; }

    public byte[] Buffer { get; set; }
}
