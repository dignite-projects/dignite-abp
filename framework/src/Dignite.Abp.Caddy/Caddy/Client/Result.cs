using System.Net;

namespace Caddy.Client;

public class Result<T>
{
    public bool IsSuccess { get; private set; }
    public string? ErrorMessage { get; private set; }
    public T? Data { get; private set; }

    public HttpStatusCode? HttpStatusCode { get; set; }

    public static Result<T> Success(T data, HttpStatusCode? httpStatusCode)
    {
        return new Result<T> { IsSuccess = true, Data = data, HttpStatusCode = httpStatusCode };
    }

    public static Result<T> Fail(string errorMessage, HttpStatusCode? httpStatusCode)
    {
        return new Result<T> { IsSuccess = false, ErrorMessage = errorMessage, HttpStatusCode = httpStatusCode };
    }
}
