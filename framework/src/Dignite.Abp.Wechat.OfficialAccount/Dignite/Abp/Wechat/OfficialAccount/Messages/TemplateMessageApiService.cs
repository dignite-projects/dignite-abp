using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dignite.Abp.Wechat.OfficialAccount.Messages;

public class TemplateMessageApiService : ApiService, ITemplateMessageApiService
{
    private int ErrCode40001Count = 0;
    private readonly IStartApiService _wechatApiService;

    public TemplateMessageApiService(IStartApiService wechatApiService)
    {
        _wechatApiService = wechatApiService;
    }



    /// <summary>
    /// 发送模板消息
    /// </summary>
    /// <param name="touser"></param>
    /// <param name="template_id"></param>
    /// <param name="data"></param>
    /// <param name="url"></param>
    /// <param name="miniProgram"></param>
    /// <returns></returns>
    public async Task<SendTemplateMessageResult> SendAsync(string touser, string template_id, IDictionary<string, TemplateMessageData> data, string url = "", TemplateMessageMiniProgram miniProgram = null)
    {
        var access_token = (await _wechatApiService.GetAccessTokenAsync()).AccessToken;

        var client = ClientFactory.CreateClient(OfficialAccountConsts.HttpClientName);

        //构建微信小程序发送订阅消息所需要的参数
        var content = new StringContent(JsonSerializer.Serialize(new {
            touser,
            template_id,
            data,
            url,
            miniProgram
        }).Replace('[', '}').Replace(']', '}'), Encoding.UTF8, "application/json");

        //调用微信发订阅消息接口
        var response = await client.PostAsync("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + access_token, content);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"调用微信统一发送消息接口异常 ({response.StatusCode}).请检查.");

        //序列化微信接口返回值
        var result = JsonSerializer.Deserialize<SendTemplateMessageResult>(await response.Content.ReadAsStringAsync());

        if (result.ErrorCode != 0)
        {
            if (result.ErrorCode == 40001 && ErrCode40001Count <= 5)
            {
                ErrCode40001Count++;
                await _wechatApiService.RefreshAccessTokenAsync();
                await SendAsync(touser, template_id, data, url, miniProgram);
            }
            //throw new Volo.Abp.UserFriendlyException($"发生错误：代码：{result.errcode.ToString()}，信息：{result.errmsg}");
        }

        return result;
    }
}
