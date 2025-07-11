using System;
using System.Threading.Tasks;
using Dignite.Abp.TenantDomain.Caddy.CaddyConfig;
using Dignite.Abp.TenantDomain.TenantRouteConfigs;
using Shouldly;
using Volo.Abp.Testing;
using Xunit;

namespace Dignite.Abp.TenantDomain.Caddy;
public class CaddyTenantConfigManagerTest : AbpIntegratedTest<AbpTenantDomainCaddyTestModule>
{
    private readonly CaddyTenantConfigManager caddyTenantConfigManager;

    public CaddyTenantConfigManagerTest()
    {
        caddyTenantConfigManager = GetRequiredService<CaddyTenantConfigManager>();
    }

    [Fact]
    public async Task FindById_IsNull()
    {
        var tenantConfig = await caddyTenantConfigManager.FindAsync(Guid.NewGuid());

        tenantConfig.ShouldBeNull();
    }

    [Fact]
    public async Task CreateFindUpdateDeleteConfig_ShouldSucceed()
    {
        var tenantConfig = new TenantRouteConfig(Guid.NewGuid(), ["test.localhost"],"www.test.com");

        // Create
        await caddyTenantConfigManager.AddOrUpdateAsync(tenantConfig);

        // Find Not Null
        tenantConfig = await caddyTenantConfigManager.FindAsync(tenantConfig.GetTenantId());
        tenantConfig.ShouldNotBeNull();
        tenantConfig.Matches[0].Hosts[0].ShouldBe("test.localhost");
        tenantConfig.Handles[0].Routes[0].Handles[0].Upstreams[0].Dial.ShouldBe("www.test.com");
        tenantConfig.Handles[0].Routes[0].Handles[0].Headers.Request!.GetTenantId().ShouldBe(tenantConfig.GetTenantId());

        // Update 
        tenantConfig.SetMatches("test.localhost", "test2.localhost");
        await caddyTenantConfigManager.UpdateAsync(tenantConfig);

        tenantConfig = await caddyTenantConfigManager.FindAsync(tenantConfig.GetTenantId());
        tenantConfig.ShouldNotBeNull();
        tenantConfig.Matches[0].Hosts[0].ShouldBe("test.localhost");
        tenantConfig.Matches[1].Hosts[0].ShouldBe("test2.localhost");
        tenantConfig.Handles[0].Routes[0].Handles[0].Upstreams[0].Dial.ShouldBe("www.test.com");
        tenantConfig.Handles[0].Routes[0].Handles[0].Headers.Request!.GetTenantId().ShouldBe(tenantConfig.GetTenantId());

        // Delete
        await caddyTenantConfigManager.DeleteAsync(tenantConfig.GetTenantId());

        tenantConfig = await caddyTenantConfigManager.FindAsync(tenantConfig.GetTenantId());
        tenantConfig.ShouldBeNull();
    }
}
