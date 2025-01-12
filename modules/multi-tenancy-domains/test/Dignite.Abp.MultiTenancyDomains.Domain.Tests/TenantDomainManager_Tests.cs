using System.Threading.Tasks;
using Dignite.Abp.MultiTenancyDomains;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;



public abstract class TenantDomainManager_Tests<TStartupModule> : MultiTenancyDomainsDomainTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly TenantDomainManager _tenantDomainManager;

    public TenantDomainManager_Tests()
    {
        _tenantDomainManager = GetRequiredService<TenantDomainManager>();
    }

    [Fact]
    public async Task Method1Async()
    {
        var entity = await _tenantDomainManager.CreateAsync("tenantDomain.com", null);
        entity.ShouldNotBeNull();
    }
}
