using System.Threading.Tasks;
using Dignite.Abp.TenantDomainManagement;
using Dignite.Abp.TenantDomainManagement.TestBase;
using Shouldly;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Xunit;

public abstract class TenantDomainAppService_Tests<TStartupModule> : TenantDomainManagementApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly ITenantDomainAppService _tenantDomainAppService;
    private readonly TenantDomainManagementTestData _tenantDomainTestData;
    private readonly ICurrentTenant _currentTenant;

    protected TenantDomainAppService_Tests()
    {
        _tenantDomainAppService = GetRequiredService<ITenantDomainAppService>();
        _tenantDomainTestData = GetRequiredService<TenantDomainManagementTestData>();
        _currentTenant = GetRequiredService<ICurrentTenant>();
    }

    [Fact]
    public async Task GetAsync()
    {
        var result = await _tenantDomainAppService.GetAsync();
        result.ShouldNotBeNull();
        result.IsValid.ShouldBe(false);
    }

    [Fact]
    public async Task ConnectAsync()
    {
        using (_currentTenant.Change(_tenantDomainTestData.Tenant1Id, _tenantDomainTestData.TenantName))
        {
            var input = new ConnectTenantDomainInput();
            input.DomainName = _tenantDomainTestData.DomainName;
            var result = await _tenantDomainAppService.ConnectAsync(input);
            result.IsValid.ShouldBe(true);
        }
    }
}
