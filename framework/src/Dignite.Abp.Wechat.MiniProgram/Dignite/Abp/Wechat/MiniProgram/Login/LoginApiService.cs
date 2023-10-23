using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Dignite.Abp.Wechat.MiniProgram.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace Dignite.Abp.Wechat.MiniProgram.Login;

public class LoginApiService : ApiService, ILoginApiService
{

    private readonly IHttpContextAccessor _accessor;

    public LoginApiService(
        IHttpContextAccessor accessor
        )
    {
        _accessor = accessor;
    }



    public async Task<MiniProgramUserSession> GetSessionTokenAsync(string code)
    {
        //Log.Information($"Start-GetSessionTokenAsync:{code}");
        //var token = await GetAccessTokenAsync();
        //Log.Information($"accessToken:{ token?.AccessToken}");
        var appId = await SettingProvider.GetOrNullAsync(WechatMiniProgramSettings.AppId);
        var secret = await SettingProvider.GetOrNullAsync(WechatMiniProgramSettings.Secret);
        var requestUrl = QueryHelpers.AddQueryString("https://api.weixin.qq.com/sns/jscode2session", new Dictionary<string, string>
        {
            { "appid",appId},
            { "secret",secret},
            { "js_code", code },
            { "grant_type", "authorization_code" }
        });
        //Log.Information($"jscode2session:{requestUrl}");
        var client = ClientFactory.CreateClient(MiniProgramConsts.HttpClientName);
        var response = await client.GetAsync(requestUrl, _accessor.HttpContext.RequestAborted);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"An error occurred when retrieving wechat mini program user information ({response.StatusCode}). Please check if the authentication information is correct and the corresponding Microsoft Account API is enabled.");

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<MiniProgramUserSession>(content, new JsonSerializerOptions());
        //Log.Information($"jscode2session-Result:{content}");
        return result;
    }
}
