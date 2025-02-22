using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Dignite.Abp.TenantDomain.Caddy;

public class FakeHttpMessageHandler : HttpMessageHandler
{
    private readonly HttpResponseMessage[] _responses;
    private int _callCount;

    public FakeHttpMessageHandler(params HttpResponseMessage[] responses)
    {
        _responses = responses;
    }

    public int NumberOfCalls => _callCount;

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_responses[Math.Min(_callCount++, _responses.Length - 1)]);
    }
}
