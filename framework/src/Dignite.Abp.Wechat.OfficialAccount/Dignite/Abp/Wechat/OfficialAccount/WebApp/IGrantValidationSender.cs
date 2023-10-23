using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Wechat.OfficialAccount.WebApp;

/// <summary>
/// 微信公众号登陆的发起者
/// </summary>
public interface IGrantValidationSender : ITransientDependency
{
    Task<GrantValidationAccessToken> ValidateAsync(string code, string state);
}
