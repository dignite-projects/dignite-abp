using System.Threading.Tasks;
using Dignite.Abp.TenantDomains;
using Dignite.Abp.TenantDomains.TestBase;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;



public abstract class TenantDomainAppService_Tests<TStartupModule> : TenantDomainsApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly ITenantDomainAppService _tenantDomainAppService;
    private readonly TenantDomainTestData _tenantDomainTestData;

    protected TenantDomainAppService_Tests()
    {
        _tenantDomainAppService = GetRequiredService<ITenantDomainAppService>();
        _tenantDomainTestData = GetRequiredService<TenantDomainTestData>();
    }

    [Fact]
    public async Task GetAsync()
    {
        var result = await _tenantDomainAppService.FindByDomainNameAsync(_tenantDomainTestData.DomainName1);
        result.ShouldNotBeNull();
    }
}
