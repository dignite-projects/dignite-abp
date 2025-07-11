using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;

namespace Dignite.Abp.TenantDomain.Caddy.CaddyConfig;
/// <summary>
/// <see href="https://caddyserver.com/docs/api#api"/>
/// </summary>
public class CaddyClient
{
    private readonly HttpClient _httpClient;

    public CaddyClient(string apiUrl, string username, string password)
    {
        // Ensure the apiUrl is the admin API endpoint (including the proper port).
        _httpClient = new HttpClient { BaseAddress = new Uri(apiUrl) };

        // Set up basic authentication.
        var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

        // By default, accept JSON responses.
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    #region Endpoints (Generic Only)

    /// <summary>
    /// POST /load: uses a Caddyfile by default.
    /// </summary>
    public async Task<RequestResult<T>> LoadConfiguration<T>(object config, string contentType = "text/caddyfile", CancellationToken cancellationToken = default)
    {
        return await SendRequest<T>("/load", HttpMethod.Post, config, contentType, cancellationToken);
    }


    /// <summary>
    /// POST /stop: This is a fire‑and‑forget endpoint (an empty response is considered success).
    /// </summary>
    public async Task<RequestResult<T>> Stop<T>(CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/stop");
            await _httpClient.SendAsync(request, cancellationToken);
            return RequestResult<T>.Success(default!, null);
        }
        catch (TaskCanceledException)
        {
            return RequestResult<T>.Success(default!, null);
        }
        catch (Exception ex)
        {
            return RequestResult<T>.Fail(ex.Message, null);
        }

    }

    /// <summary>
    /// GET /config/[path]: uses JSON.
    /// </summary>
    public async Task<RequestResult<T>> GetConfig<T>(string path = "", CancellationToken cancellationToken = default)
    {
        return await SendRequest<T>($"/config/{path}", HttpMethod.Get, null, "application/json", cancellationToken);
    }

    /// <summary>
    /// POST /config/[path]: uses JSON.
    /// </summary>
    public async Task<RequestResult<T>> SetConfig<T>(string path, object config, CancellationToken cancellationToken = default)
    {
        return await SendRequest<T>($"/config/{path}", HttpMethod.Put, config, "application/json", cancellationToken);
    }

    /// <summary>
    /// PUT /config/[path]: uses JSON.
    /// </summary>
    public async Task<RequestResult<T>> CreateConfig<T>(string path, object config, CancellationToken cancellationToken = default)
    {
        return await SendRequest<T>($"/config/{path}?persist=true", HttpMethod.Post, config, "application/json", cancellationToken);
    }

    /// <summary>
    /// PATCH /config/[path]: uses JSON.
    /// </summary>
    public async Task<RequestResult<T>> UpdateConfig<T>(string path, object config, CancellationToken cancellationToken = default)
    {
        return await SendRequest<T>($"/config/{path}", new HttpMethod("PATCH"), config, "application/json", cancellationToken);
    }

    /// <summary>
    /// DELETE /config/[path]: uses JSON.
    /// </summary>
    public async Task<RequestResult<T>> DeleteConfig<T>(string path, CancellationToken cancellationToken = default)
    {
        return await SendRequest<T>($"/config/{path}", HttpMethod.Delete, null, "application/json", cancellationToken);
    }

    public async Task<RequestResult<T>> GetByIdAsync<T>(string id, string path = "", CancellationToken cancellationToken = default)
    {
        return await SendRequest<T>($"/id/{id}/{path}", HttpMethod.Get, null, "application/json", cancellationToken);
    }
    public async Task<RequestResult<T>> DeleteByIdAsync<T>(string id, string path = "", CancellationToken cancellationToken = default)
    {
        return await SendRequest<T>($"/id/{id}/{path}", HttpMethod.Delete, null, "application/json", cancellationToken);
    }

    public async Task<RequestResult<T>> UpdateByIdAsync<T>(string id, object config, string path = "", CancellationToken cancellationToken = default)
    {
        return await SendRequest<T>($"/id/{id}/{path}", new HttpMethod("PATCH"), config, "application/json", cancellationToken);
    }

    /// <summary>
    /// POST /adapt: uses a Caddyfile by default.
    /// </summary>
    public async Task<RequestResult<T>> AdaptConfig<T>(object config, string contentType = "text/caddyfile", CancellationToken cancellationToken = default)
    {
        return await SendRequest<T>("/adapt", HttpMethod.Post, config, contentType, cancellationToken);
    }

    /// <summary>
    /// GET /pki/ca/<id>: uses JSON.
    /// </summary>
    public async Task<RequestResult<T>> GetCAInfo<T>(string caId, CancellationToken cancellationToken = default)
    {
        return await SendRequest<T>($"/pki/ca/{caId}", HttpMethod.Get, null, "application/json", cancellationToken);
    }

    /// <summary>
    /// GET /pki/ca/<id>/certificates: uses JSON.
    /// </summary>
    public async Task<RequestResult<T>> GetCACertificates<T>(string caId, CancellationToken cancellationToken = default)
    {
        return await SendRequest<T>($"/pki/ca/{caId}/certificates", HttpMethod.Get, null, "application/json", cancellationToken);
    }

    /// <summary>
    /// GET /reverse_proxy/upstreams: uses JSON.
    /// </summary>
    public async Task<RequestResult<T>> GetReverseProxyUpstreams<T>(CancellationToken cancellationToken = default)
    {
        return await SendRequest<T>("/reverse_proxy/upstreams", HttpMethod.Get, null, "application/json", cancellationToken);
    }



    #endregion

    #region Internal Helper

    private async Task<RequestResult<T>> SendRequest<T>(
        string endpoint,
        HttpMethod method,
        object? content = null,
        string contentType = "application/json",
        CancellationToken cancellationToken = default)
    {
        HttpStatusCode? statusCode = null;

        try
        {
            var request = new HttpRequestMessage(method, endpoint);

            if (content != null)
            {
                StringContent stringContent;
                // For raw Caddyfile text, do not JSON‑serialize.
                if (content is string s && contentType.Equals("text/caddyfile", StringComparison.OrdinalIgnoreCase))
                {
                    stringContent = new StringContent(s, Encoding.UTF8, contentType);
                }
                else
                {
                    var jsonContent = JsonConvert.SerializeObject(content);
                    stringContent = new StringContent(jsonContent, Encoding.UTF8, contentType);
                }
                request.Content = stringContent;
            }

            var response = await _httpClient.SendAsync(request, cancellationToken);
            statusCode = response.StatusCode;
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(responseData))
            {
                if (typeof(T) == typeof(string))
                {
                    return RequestResult<T>.Success((T)(object)"", statusCode);
                }
                return RequestResult<T>.Success(default, statusCode);
            }

            if (typeof(T) == typeof(string))
            {
                return RequestResult<T>.Success((T)(object)responseData, statusCode);
            }

            var data = JsonConvert.DeserializeObject<T>(responseData);
            return RequestResult<T>.Success(data, statusCode);
        }
        catch (Exception ex)
        {
            return RequestResult<T>.Fail(ex.Message, statusCode);
        }
    }

    #endregion
}
