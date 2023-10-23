using Microsoft.AspNetCore.Http;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dignite.Abp.Wechat.OfficialAccount.WebApp;

/// <summary>
/// 用户生成JSAPI的签名
/// </summary>
public class JsapiSignatureMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebAppApiService _officAccountApiService;

    public JsapiSignatureMiddleware(RequestDelegate next, IWebAppApiService officAccountApiService)
    {
        _next = next;
        _officAccountApiService = officAccountApiService;
    }

    public async Task Invoke(HttpContext context)
    {
        var request = context.Request;
        var response = context.Response;


        //若当前请求不是获取api_ticket的地址，则跳过处理，直接执行后续中间件
        if (!OfficialAccountConsts.JsapiSignaturePath.Equals(request.Path, StringComparison.OrdinalIgnoreCase))
        {
            await _next(context);
            return;
        }

        var noncestr = Guid.NewGuid().ToString("N");
        var timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        var url = context.Request.Headers["Referer"].ToString();
        var jsapi_ticket = await _officAccountApiService.GetJsapiTicketAsync();
        var str = $"jsapi_ticket={jsapi_ticket.Ticket}&noncestr={noncestr}&timestamp={timestamp}&url={url}";
        //str demo:"jsapi_ticket=sM4AOVdWfPE4DxkXGEs8VMCPGGVi4C3VM0P37wVUCFvkVAy_90u5h9nbSlYy3-Sl-HhTdfl2fzFy1AOcHKP7qg&noncestr=Wm3WZYTPz0wzccnW&timestamp=1414587457&url=http://mp.weixin.qq.com?params=value";

        //sha1加密
        using (var sha1Hash = SHA1.Create())
        {
            //From String to byte array
            var sourceBytes = Encoding.UTF8.GetBytes(str);
            var hashBytes = sha1Hash.ComputeHash(sourceBytes);
            var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

            var jsapiParam = new {
                noncestr,
                timestamp,
                url,
                signature = hash.ToLower()
            };

            await response.WriteAsync(JsonSerializer.Serialize(jsapiParam));
        }
    }
}
