using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dignite.Abp.Wechat.MiniProgram.Security;

public class SecurityApiService : ApiService, ISecurityApiService
{
    private int ErrCode40001Count = 0;

    private readonly IStartApiService _startApiService;

    public SecurityApiService(
        IStartApiService startApiService
        )
    {
        _startApiService = startApiService;
    }



    public async Task<WechatResult> MessageSecurityCheckAsync(string content)
    {
        var access_token = (await _startApiService.GetAccessTokenAsync()).AccessToken;

        var contents = new StringContent(JsonSerializer.Serialize(new { content }), Encoding.UTF8, "application/json");

        var client = ClientFactory.CreateClient(MiniProgramConsts.HttpClientName);
        var response = await client.PostAsync("https://api.weixin.qq.com/wxa/msg_sec_check?access_token=" + access_token, contents);

        var result = JsonSerializer.Deserialize<WechatResult>(await response.Content.ReadAsStringAsync());
        if (result.ErrorCode != 0)
        {
            if (result.ErrorCode == 40001 && ErrCode40001Count <= 5)
            {
                ErrCode40001Count++;
                await _startApiService.RefreshAccessTokenAsync();
                await MessageSecurityCheckAsync(content);
            }
        }
        return result;
    }

}
