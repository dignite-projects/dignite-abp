using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.LocaleManagement.Host.Data;

public class LocaleManagementHostEFCoreDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public LocaleManagementHostEFCoreDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the LocaleManagementDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<LocaleManagementHostDbContext>()
            .Database
            .MigrateAsync();
    }
}
