
using System.Text.Json.Serialization;

namespace Dignite.Abp.Wechat.OfficialAccount.WebApp;

public class GrantValidationAccessToken
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("scope")]
    public string Scope { get; set; }
}
