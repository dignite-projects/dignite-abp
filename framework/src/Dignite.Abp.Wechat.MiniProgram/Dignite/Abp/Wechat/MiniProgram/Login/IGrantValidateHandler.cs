
using System.Threading.Tasks;

namespace Dignite.Abp.Wechat.MiniProgram.Login;

/// <summary>
/// 在<see cref="MiniprogramGrantValidator.ValidateAsync(IdentityServer4.Validation.ExtensionGrantValidationContext)"/>获取微信小程序用户的信息后，向本接口传递上下文信息，按项目需要定制自己的登陆逻辑。
/// </summary>
public interface IGrantValidateHandler
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ct">包含微信用户信息和其它相关信息的上下文对象</param>
    /// <returns>返回向登陆用户附加的<see cref="System.Security.Claims.Claim"/>信息</returns>
    Task<GrantValidationResult> ExcuteAsync(GrantValidationContext ct);
}
