using System.Net;

namespace Dignite.Abp.TenantDomain.Caddy.CaddyConfig;

public class RequestResult<T>
{
    public bool IsSuccess { get; private set; }
    public string? ErrorMessage { get; private set; }
    public T? Data { get; private set; }

    public HttpStatusCode? HttpStatusCode { get; set; }

    public static RequestResult<T> Success(T data, HttpStatusCode? httpStatusCode)
    {
        return new RequestResult<T> { IsSuccess = true, Data = data, HttpStatusCode = httpStatusCode };
    }

    public static RequestResult<T> Fail(string errorMessage, HttpStatusCode? httpStatusCode)
    {
        return new RequestResult<T> { IsSuccess = false, ErrorMessage = errorMessage, HttpStatusCode = httpStatusCode };
    }
}
