using System.Net;
using System.Threading.Tasks;
using System;
using Caddy.Client;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.TenantDomain.TenantRouteConfigs;

public class CaddyTenantConfigManager(CaddyClient client) : ITransientDependency
{
    private const string Path = "apps/http/servers/https";

    public async Task AddOrUpdateAsync(TenantRouteConfig config)
    {
        if (await ExistAsync(config.GetTenantId()))
        {
            await UpdateAsync(config);
        }

        await CreateAsync(config);
    }

    public async Task CreateAsync(TenantRouteConfig config)
    {
        var result = await client.CreateConfig<string>(Path + "/routes", config);
        if (!result.IsSuccess)
        {
            throw new UserFriendlyException("Caddy 租户配置修创建败:" + result.ErrorMessage);
        }
    }

    public async Task UpdateAsync(TenantRouteConfig config)
    {
        var configId = config.Id;
        var result = await client.UpdateByIdAsync<string>(configId, config);

        if (result.HttpStatusCode == HttpStatusCode.NotFound)
        {
            throw new UserFriendlyException("Caddy 不存在的租户配置:" + result.ErrorMessage);
        }

        if (!result.IsSuccess)
        {
            throw new UserFriendlyException("Caddy 租户配置修改失败:" + result.ErrorMessage);
        }
    }

    public async Task DeleteAsync(Guid? tenantId)
    {
        var configId = GetConfigId(tenantId);

        var result = await client.DeleteByIdAsync<string>(configId);

        if (result.HttpStatusCode == HttpStatusCode.NotFound)
        {
            return;
        }

        if (!result.IsSuccess)
        {
            throw new UserFriendlyException("Caddy 租户配置删除失败:" + result.ErrorMessage);
        }
    }

    public async Task<bool> ExistAsync(Guid? tenantId)
    {
        return await FindAsync(tenantId) != null;
    }

    public async Task<TenantRouteConfig?> FindAsync(Guid? tenantId)
    {
        var configId = GetConfigId(tenantId);
        var result = await client.GetByIdAsync<TenantRouteConfig>(configId);
        if (result.HttpStatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        if (!result.IsSuccess)
        {
            throw new UserFriendlyException("Caddy " + result.ErrorMessage);
        }

        return result.Data;
    }

    protected string GetConfigId(Guid? tenantId)
    {
        return "tenantId_" + tenantId;
    }
}
