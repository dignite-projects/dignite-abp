using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Wechat.OfficialAccount.Messages;

/// <summary>
/// 公众号模板消息
/// </summary>
public interface ITemplateMessageApiService : ITransientDependency
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="touser">接收者openid</param>
    /// <param name="template_id">模板ID</param>
    /// <param name="data">模板数据</param>
    /// <param name="url">模板跳转链接（海外帐号没有跳转能力）</param>
    /// <param name="miniProgram">跳小程序所需数据，不需跳小程序可不用传该数据</param>
    /// <remarks>
    /// url和miniprogram都是非必填字段，若都不传则模板无跳转；若都传，会优先跳转至小程序。开发者可根据实际需要选择其中一种跳转方式即可。当用户的微信客户端版本不支持跳小程序时，将会跳转至url。
    /// </remarks>
    /// <returns></returns>
    Task<SendTemplateMessageResult> SendAsync(string touser, string template_id, IDictionary<string, TemplateMessageData> data, string url = "", TemplateMessageMiniProgram miniProgram = null);
}
