using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Wechat.MiniProgram;

public interface IStartApiService : ITransientDependency
{
    /// <summary>
    /// 获取微信小程序的全局AccessToken
    /// </summary>
    /// <returns></returns>
    Task<GlobalAccessToken> GetAccessTokenAsync();

    /// <summary>
    /// 刷新微信小程序的全局AccessToken
    /// </summary>
    /// <returns></returns>
    Task<GlobalAccessToken> RefreshAccessTokenAsync();
}
