using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.RegionalizationManagement.Host.Data;

public class RegionalizationManagementHostEFCoreDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public RegionalizationManagementHostEFCoreDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the RegionalizationManagementDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<RegionalizationManagementHostDbContext>()
            .Database
            .MigrateAsync();
    }
}
