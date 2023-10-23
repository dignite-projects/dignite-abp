using Dignite.Abp.Wechat.OfficialAccount.IdentityServer;
using Dignite.Abp.Wechat.OfficialAccount.IdentityServer.Settings;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace Dignite.Abp.Wechat.OfficialAccount.WebApp;

/// <summary>
/// 微信公众号程序在拿到code和state后，向IdentityServer发起登记请求
/// </summary>
public class IdentityServerGrantValidationSender : IGrantValidationSender, ITransientDependency
{
    private readonly ISettingProvider _settingProvider;
    private readonly IHttpClientFactory _clientFactory;
    private readonly IHttpContextAccessor _accessor;

    public IdentityServerGrantValidationSender(ISettingProvider settingProvider, IHttpClientFactory clientFactory, IHttpContextAccessor accessor)
    {
        _settingProvider = settingProvider;
        _clientFactory = clientFactory;
        _accessor = accessor;
    }

    public async Task<GrantValidationAccessToken> ValidateAsync(string code, string state)
    {
        var grant_type = IdentityServerConsts.WechatOfficialAccountGrantType;
        var client_id = await _settingProvider.GetOrNullAsync(IdentityServerSettings.ClientId);
        var client_secret = await _settingProvider.GetOrNullAsync(IdentityServerSettings.ClientSecret);

        var client = _clientFactory.CreateClient(OfficialAccountConsts.HttpClientName);
        HttpContent content = new FormUrlEncodedContent(new[]
        {
                new KeyValuePair<string, string>("grant_type", grant_type),
                new KeyValuePair<string, string>("client_id", client_id),
                new KeyValuePair<string, string>("client_secret", client_secret),
                new KeyValuePair<string, string>("state", state),
                new KeyValuePair<string, string>("code", code)
            });

        content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");


        /* 
         * 在绑定已登陆账户时，需要将已登陆用户的TOKEN传递给 connect/token ，用于 IWebAppGrantValidationSender 中获取当前用户
        */
        var requestToken = _accessor.HttpContext.Request.Headers["authorization"];
        if (requestToken.Any())
        {
            var token = requestToken[0].Replace("Bearer ", "");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }


        var url = GetAbsoluteUri(_accessor.HttpContext.Request);
        var response = await client.PostAsync(
            url + "/connect/token",
            content,
            _accessor.HttpContext.RequestAborted
            );
        if (response.IsSuccessStatusCode)
        {
            var msg = await response.Content.ReadAsStringAsync();
            var validationResult = JsonSerializer.Deserialize<GrantValidationAccessToken>(msg);
            return validationResult;
        }
        else
        {
            throw new AbpException(response.StatusCode.ToString());
        }
    }

    private string GetAbsoluteUri(HttpRequest request)
    {
        return new StringBuilder()
            .Append(request.Scheme)
            .Append("://")
            .Append(request.Host.Value)
            .ToString();
    }
}