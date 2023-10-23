using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Wechat.MiniProgram.Login;

public interface ILoginApiService : ITransientDependency
{

    /// <summary>
    /// 获取小程序的Session
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    Task<MiniProgramUserSession> GetSessionTokenAsync(string code);

}
