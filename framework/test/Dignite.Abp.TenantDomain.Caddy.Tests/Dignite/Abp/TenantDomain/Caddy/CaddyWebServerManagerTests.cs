using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Volo.Abp;
using Volo.Abp.Testing;
using Xunit;

namespace Dignite.Abp.TenantDomain.Caddy;
public class CaddyWebServerManagerTests : AbpIntegratedTest<AbpTenantDomainCaddyTestModule>
{
    private readonly IWebServerManager _caddyWebServerManager;
    private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
    private readonly Mock<ILogger<CaddyWebServerManager>> _loggerMock;
    private readonly Mock<IOptions<AbpTenantDomainCaddyOptions>> _optionsMock;

    public CaddyWebServerManagerTests()
    {
        _httpClientFactoryMock = new Mock<IHttpClientFactory>();
        _loggerMock = new Mock<ILogger<CaddyWebServerManager>>();
        _optionsMock = new Mock<IOptions<AbpTenantDomainCaddyOptions>>();

        _optionsMock.Setup(o => o.Value).Returns(new AbpTenantDomainCaddyOptions
        {
            ApiEndpoint = "http://localhost:2019"
        });

        _caddyWebServerManager = new CaddyWebServerManager(
            _httpClientFactoryMock.Object,
            _loggerMock.Object,
            _optionsMock.Object
        );
    }

    protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
    {
        options.UseAutofac();
    }

    [Fact]
    public async Task AddDomainToSite_Should_Add_Domain_When_Not_Exists()
    {
        var domain = "example.com";
        var upstreamAddress = "localhost:5000";
        var tenantId = Guid.NewGuid();

        // 使用多行字符串确保 JSON 格式清晰
        var jsonConfig = @"{
            ""apps"": {
                ""http"": {
                    ""servers"": {
                        ""default"": {
                            ""listen"": [],
                            ""routes"": []
                        }
                    }
                }
            }
        }";
        var fakeHandler = new FakeHttpMessageHandler(
        new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(jsonConfig, Encoding.UTF8, "application/json")
        },
        new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK
        });

        var httpClient = new HttpClient(fakeHandler);
        _httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

        await _caddyWebServerManager.AddOrUpdateDomainAsync(domain, upstreamAddress, tenantId);

        Assert.Equal(2, fakeHandler.NumberOfCalls);
    }

    [Fact]
    public async Task RemoveDomainFromSite_Should_Remove_Domain_When_Exists()
    {
        var tenantId = Guid.NewGuid();
        var fakeHandler = new FakeHttpMessageHandler(
            new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"apps\":{\"http\":{\"servers\":{\"default\":{\"listen\":[\":443\"],\"routes\":[{\"match\":[{\"host\":[\"example.com\"]}]}]}}}}}") // 修正为对象格式
            },
            new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
            });

        var httpClient = new HttpClient(fakeHandler);
        _httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

        await _caddyWebServerManager.RemoveDomainAsync(tenantId);

        Assert.Equal(2, fakeHandler.NumberOfCalls);
    }
}