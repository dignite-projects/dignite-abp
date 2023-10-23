
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Dignite.Abp.Wechat.MiniProgram.Settings;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.Caching;

namespace Dignite.Abp.Wechat.MiniProgram;

public class StartApiService : ApiService, IStartApiService
{
    private const string accessTokenCacheKey = "dignite-wechat-miniprogram-accesstoken-cache";

    private readonly IDistributedCache<GlobalAccessToken> _accessTokenCache;

    public StartApiService(
        IDistributedCache<GlobalAccessToken> accessTokenCache
        )
    {
        _accessTokenCache = accessTokenCache;
    }


    /// <summary>
    /// 获取微信小程序的全局AccessToken
    /// </summary>
    /// <returns></returns>
    public async Task<GlobalAccessToken> GetAccessTokenAsync()
    {
        var token = await _accessTokenCache.GetAsync(accessTokenCacheKey);
        if (token == null)
        {
            token = await GetAccessTokenFromApiAsync();
            await _accessTokenCache.SetAsync(accessTokenCacheKey, token,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(token.ExpiresIn - 10)
                });
        }

        return token;
    }

    /// <summary>
    /// 刷新微信小程序的全局AccessToken
    /// </summary>
    /// <returns></returns>
    public async Task<GlobalAccessToken> RefreshAccessTokenAsync()
    {
        await _accessTokenCache.RemoveAsync(accessTokenCacheKey);
        return await GetAccessTokenAsync();
    }



    private async Task<GlobalAccessToken> GetAccessTokenFromApiAsync()
    {
        var requestUrl = QueryHelpers.AddQueryString("https://api.weixin.qq.com/cgi-bin/token",
             new Dictionary<string, string>
            {
                { "grant_type", "client_credential" },
                { "appid", await SettingProvider.GetOrNullAsync(WechatMiniProgramSettings.AppId) },
                { "secret", await SettingProvider.GetOrNullAsync(WechatMiniProgramSettings.Secret) }
            });

        var client = ClientFactory.CreateClient(Wechat.MiniProgram.MiniProgramConsts.HttpClientName);
        var response = await client.GetAsync(requestUrl);
        var msg = await response.Content.ReadAsStringAsync();
        //Log.Information($"GetAccessTokenFromApiAsync-Result:{msg}");
        var result = JsonSerializer.Deserialize<GlobalAccessToken>(msg, new JsonSerializerOptions());
        //Log.Information($"GetAccessTokenFromApiAsync-Obj-{Newtonsoft.Json.JsonConvert.SerializeObject(result)}");
        return result;
    }

}
