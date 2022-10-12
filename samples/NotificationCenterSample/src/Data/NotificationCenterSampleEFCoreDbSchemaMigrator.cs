using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

namespace NotificationCenterSample.Data;

public class NotificationCenterSampleEFCoreDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public NotificationCenterSampleEFCoreDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the NotificationCenterSampleDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<NotificationCenterSampleDbContext>()
            .Database
            .MigrateAsync();
    }
}
