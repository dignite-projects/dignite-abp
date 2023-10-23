
using System.Threading.Tasks;

namespace Dignite.Abp.Wechat.OfficialAccount.WebApp;

/// <summary>
/// 在<see cref="WebAppGrantValidator.ValidateAsync"/>获取微信公众号用户的信息后，向本接口传递上下文信息，按项目需要定制自己的登陆逻辑。
/// </summary>
public interface IGrantValidationHandler
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ct">包含微信用户信息和其它相关信息的上下文对象</param>
    /// <returns>返回向登陆用户附加的<see cref="System.Security.Claims.Claim"/>信息</returns>
    Task<GrantValidationResult> ExcuteAsync(GrantValidationContext ct);
}
