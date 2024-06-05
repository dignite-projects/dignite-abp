using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

namespace DevSample.Data;

public class DevSampleEFCoreDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public DevSampleEFCoreDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the DevSampleDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<DevSampleDbContext>()
            .Database
            .MigrateAsync();
    }
}
