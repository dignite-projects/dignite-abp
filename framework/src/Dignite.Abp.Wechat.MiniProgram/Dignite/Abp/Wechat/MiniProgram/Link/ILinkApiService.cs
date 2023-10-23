using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Wechat.MiniProgram.Link;

public interface ILinkApiService : ITransientDependency
{

    /// <summary>
    /// 获取小程序页面的二维码
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    Task<QrCode> GetQrCode(string path);
}
