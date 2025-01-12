using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.MultiTenancyDomains.Host.Data;

public class MultiTenancyDomainsHostEFCoreDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public MultiTenancyDomainsHostEFCoreDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the TenantDomainsDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<MultiTenancyDomainsHostDbContext>()
            .Database
            .MigrateAsync();
    }
}
