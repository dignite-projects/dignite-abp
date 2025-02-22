using System;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.OpenIddict.Applications;
using Xunit;

namespace Dignite.Abp.TenantDomain.OpenIddict;
public class OpenIddictAuthServerRedirectUriManagerTests : OpenIddictTestBase<AbpTenantDomainOpenIddictTestModule>
{
    private readonly IAuthServerRedirectUriManager _authServerRedirectUriManager;
    private readonly AbpOpenIddictTestData _testData;
    private readonly IAbpApplicationManager _applicationManager;

    public OpenIddictAuthServerRedirectUriManagerTests()
    {
        _authServerRedirectUriManager = GetRequiredService<IAuthServerRedirectUriManager>(); 
        _testData = GetRequiredService<AbpOpenIddictTestData>();
        _applicationManager = GetRequiredService<IAbpApplicationManager>();
    }

    [Fact]
    public async Task AddRedirectDomainAsync_Should_Add_Domain_When_Not_Exists()
    {
        var domain = "example.com";
        await _authServerRedirectUriManager.AddRedirectDomainAsync(_testData.App1ClientId, domain);

        var app1 = (await _applicationManager.FindByClientIdAsync(_testData.App1ClientId)).As<OpenIddictApplicationModel>();

        var descriptor = new AbpApplicationDescriptor();
        await _applicationManager.PopulateAsync(descriptor, app1);

        descriptor.RedirectUris.Count.ShouldBe(2);
    }

    [Fact]
    public async Task RemoveRedirectDomainAsync_Should_Remove_Domain_When_Exists()
    {
        await _authServerRedirectUriManager.RemoveRedirectDomainAsync(_testData.App1ClientId, "abp.io");

        var app1 = (await _applicationManager.FindByClientIdAsync(_testData.App1ClientId)).As<OpenIddictApplicationModel>();

        var descriptor = new AbpApplicationDescriptor();
        await _applicationManager.PopulateAsync(descriptor, app1);

        descriptor.RedirectUris.Count.ShouldBe(0);
    }
}