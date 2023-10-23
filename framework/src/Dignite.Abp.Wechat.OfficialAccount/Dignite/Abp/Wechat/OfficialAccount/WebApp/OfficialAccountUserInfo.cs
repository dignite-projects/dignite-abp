using System;
using System.Text.Json.Serialization;

namespace Dignite.Abp.Wechat.OfficialAccount.WebApp;

/// <summary>
/// 通过OAuth的获取到的用户信息（snsapi_userinfo=scope）
/// </summary>
[Serializable]
public class OfficialAccountUserInfo
{
    /// <summary>
    /// 用户的唯一标识
    /// </summary>
    [JsonPropertyName("openid")]
    public string OpenId { get; set; }
    /// <summary>
    /// 用户昵称
    /// </summary>
    [JsonPropertyName("nickname")]
    public string NickName { get; set; }
    /// <summary>
    /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
    /// </summary>
    [JsonPropertyName("sex")]
    public int Sex { get; set; }
    /// <summary>
    /// 用户个人资料填写的省份
    /// </summary>
    [JsonPropertyName("province")]
    public string Province { get; set; }
    /// <summary>
    /// 普通用户个人资料填写的城市
    /// </summary>
    [JsonPropertyName("city")]
    public string City { get; set; }
    /// <summary>
    /// 国家，如中国为CN
    /// </summary>
    [JsonPropertyName("country")]
    public string Country { get; set; }
    /// <summary>
    /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空
    /// </summary>
    [JsonPropertyName("headimgurl")]
    public string HeadImgUrl { get; set; }
    /// <summary>
    /// 用户特权信息，json 数组，如微信沃卡用户为（chinaunicom）
    /// 作者注：其实这个格式称不上JSON，只是个单纯数组。
    /// </summary>
    [JsonPropertyName("privilege")]
    public string[] Privilege { get; set; }
    /// <summary>
    /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。
    /// 详见：https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&amp;t=resource/res_list&verify=1&amp;lang=zh_CN
    /// </summary>
    [JsonPropertyName("unionid")]
    public string UnionId { get; set; }
}
