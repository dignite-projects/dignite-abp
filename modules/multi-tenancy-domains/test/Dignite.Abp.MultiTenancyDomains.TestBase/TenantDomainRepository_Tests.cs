using System.Threading.Tasks;
using Dignite.Abp.MultiTenancyDomains;
using Dignite.Abp.MultiTenancyDomains.TestBase;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;



/* Write your custom repository tests like that, in this project, as abstract classes.
 * Then inherit these abstract classes from EF Core & MongoDB test projects.
 * In this way, both database providers are tests with the same set tests.
 */
public abstract class TenantDomainRepository_Tests<TStartupModule> : MultiTenancyDomainsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly ITenantDomainRepository _tenantDomainRepository;
    private readonly MultiTenancyDomainsTestData _tenantDomainTestData;

    protected TenantDomainRepository_Tests()
    {
        _tenantDomainRepository = GetRequiredService<ITenantDomainRepository>();
        _tenantDomainTestData = GetRequiredService<MultiTenancyDomainsTestData>();
    }

    [Fact]
    public async Task FindByTenantIdAsync()
    {
        var result = await _tenantDomainRepository.FindByTenantIdAsync(null);
        result.ShouldNotBeNull();
    }
}
