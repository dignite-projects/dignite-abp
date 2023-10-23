using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Wechat.MiniProgram.Login;

/// <summary>
/// 微信小程序在拿到code和user-info后，向授权中心发起登录
/// </summary>
public interface IGrantValidationSender : ITransientDependency
{
    Task<GrantValidationAccessToken> ValidateAsync(string code, string userInfo);
}
