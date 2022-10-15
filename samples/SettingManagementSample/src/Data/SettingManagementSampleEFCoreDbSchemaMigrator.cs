using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

namespace SettingManagementSample.Data;

public class SettingManagementSampleEFCoreDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public SettingManagementSampleEFCoreDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the SettingManagementSampleDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<SettingManagementSampleDbContext>()
            .Database
            .MigrateAsync();
    }
}
