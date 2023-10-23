namespace Dignite.Abp.Wechat.MiniProgram.Message;

public class SubscribeMessageData
{
    public object value { get; set; }

    public SubscribeMessageData(object value)
    {
        this.value = value;
    }
}
