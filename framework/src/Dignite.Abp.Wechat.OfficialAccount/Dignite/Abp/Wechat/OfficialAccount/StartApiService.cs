using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Dignite.Abp.Wechat.OfficialAccount.Settings;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp;
using Volo.Abp.Caching;

namespace Dignite.Abp.Wechat.OfficialAccount;

public class StartApiService : ApiService, IStartApiService
{
    private const string accessTokenCacheKey = "dignite-wechat-mp-accesstoken-cache";

    private readonly IDistributedCache<GlobalAccessToken> _accessTokenCache;

    public StartApiService(IDistributedCache<GlobalAccessToken> accessTokenCache)
    {
        _accessTokenCache = accessTokenCache;
    }

    /// <summary>
    /// 获取微信公众号全局AccessToken
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
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(token.ExpiresIn)
                });
        }

        return token;
    }

    /// <summary>
    /// 刷新公众号AccessToken
    /// </summary>
    /// <returns></returns>
    public async Task<GlobalAccessToken> RefreshAccessTokenAsync()
    {
        await _accessTokenCache.RemoveAsync(accessTokenCacheKey);
        return await GetAccessTokenAsync();
    }

    #region private methods
    private async Task<GlobalAccessToken> GetAccessTokenFromApiAsync()
    {
        var requestUrl = QueryHelpers.AddQueryString("https://api.weixin.qq.com/cgi-bin/token",
             new Dictionary<string, string>
            {
            { "grant_type", "client_credential" },
            { "appid", await SettingProvider.GetOrNullAsync(WechatOfficialAccountSettings.AppId) },
            { "secret", await SettingProvider.GetOrNullAsync(WechatOfficialAccountSettings.Secret) }
            });

        var client = ClientFactory.CreateClient(OfficialAccountConsts.HttpClientName);

        var response = await client.GetAsync(requestUrl);
        var msg = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<GlobalAccessToken>(msg);

        if (result.ErrorCode != 0)
        {
            throw new BusinessException(WechatOfficialAccountErrorCodes.OfficialAccount.prefix + result.ErrorCode);
        }

        return result;
    }
    #endregion
}