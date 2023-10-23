using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Wechat.OfficialAccount;

public interface IStartApiService : ITransientDependency
{
    /// <summary>
    /// 获取微信公众号全局AccessToken
    /// </summary>
    /// <returns></returns>
    Task<GlobalAccessToken> GetAccessTokenAsync();

    /// <summary>
    /// 刷新公众号AccessToken
    /// </summary>
    /// <returns></returns>
    Task<GlobalAccessToken> RefreshAccessTokenAsync();
}
