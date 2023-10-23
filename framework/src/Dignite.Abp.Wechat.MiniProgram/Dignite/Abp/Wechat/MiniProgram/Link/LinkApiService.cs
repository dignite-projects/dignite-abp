using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dignite.Abp.Wechat.MiniProgram.Link;

public class LinkApiService : ApiService, ILinkApiService
{
    private readonly IStartApiService _startApiService;

    public LinkApiService(IStartApiService startApiService)
    {
        _startApiService = startApiService;
    }


    /// <summary>
    /// 获取小程序页面的二维码
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public async Task<QrCode> GetQrCode(string path)
    {
        var access_token = (await _startApiService.GetAccessTokenAsync()).AccessToken;

        var parameters = new StringContent(JsonSerializer.Serialize(new { path }), Encoding.UTF8, "application/json");

        var client = ClientFactory.CreateClient(MiniProgramConsts.HttpClientName);
        var response = await client.PostAsync("https://api.weixin.qq.com/wxa/getwxacode?access_token=" + access_token, parameters);

        try
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<QrCode>(content);
            return result;
        }
        catch
        {
            var qrCodeResult = new QrCode();
            qrCodeResult.Buffer = await response.Content.ReadAsByteArrayAsync();
            return qrCodeResult;
        }
    }
}
