using System.Threading.Tasks;
using Dignite.Abp.MultiTenancyDomains;
using Dignite.Abp.MultiTenancyDomains.TestBase;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;



public abstract class TenantDomainAppService_Tests<TStartupModule> : MultiTenancyDomainsApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly ITenantDomainAppService _tenantDomainAppService;
    private readonly MultiTenancyDomainsTestData _tenantDomainTestData;

    protected TenantDomainAppService_Tests()
    {
        _tenantDomainAppService = GetRequiredService<ITenantDomainAppService>();
        _tenantDomainTestData = GetRequiredService<MultiTenancyDomainsTestData>();
    }

    [Fact]
    public async Task GetAsync()
    {
        var result = await _tenantDomainAppService.FindByDomainNameAsync(_tenantDomainTestData.DomainName1);
        result.ShouldNotBeNull();
    }
}
