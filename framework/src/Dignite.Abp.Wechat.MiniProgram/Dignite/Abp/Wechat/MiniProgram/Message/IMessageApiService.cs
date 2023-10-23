using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Wechat.MiniProgram.Message;

public interface IMessageApiService : ITransientDependency
{
    /// <summary>
    /// 发送订阅消息
    /// </summary>
    /// <param name="touser">接收者（用户）的 openid</param>
    /// <param name="template_id">所需下发的订阅模板id</param>
    /// <param name="data">模板内容，格式形如 { "key1": { "value": any }, "key2": { "value": any } }的object</param>
    /// <param name="page">点击模板卡片后的跳转页面，仅限本小程序内的页面。支持带参数,（示例index?foo=bar）。该字段不填则模板无跳转</param>
    /// <returns></returns>
    Task<WechatResult> SendSubscribeMessageAsync(string touser, string template_id, IDictionary<string, SubscribeMessageData> data, string page = null);
}
