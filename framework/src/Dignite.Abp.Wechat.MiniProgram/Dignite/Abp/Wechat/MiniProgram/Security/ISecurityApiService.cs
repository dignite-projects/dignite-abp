using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Wechat.MiniProgram.Security;

public interface ISecurityApiService : ITransientDependency
{
    /// <summary>
    /// 内容安全检测
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    Task<WechatResult> MessageSecurityCheckAsync(string content);
}
