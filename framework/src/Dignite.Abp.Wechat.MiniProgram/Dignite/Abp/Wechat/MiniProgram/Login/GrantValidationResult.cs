using System.Collections.Generic;
using System.Security.Claims;

namespace Dignite.Abp.Wechat.MiniProgram.Login;

public class GrantValidationResult : WechatResult
{
    public string UserId { get; set; }

    public List<Claim> AddedClaims { get; set; }
}
