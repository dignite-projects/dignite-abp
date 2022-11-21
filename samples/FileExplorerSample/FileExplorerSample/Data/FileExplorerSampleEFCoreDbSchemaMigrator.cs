using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

namespace FileExplorerSample.Data;

public class FileExplorerSampleEFCoreDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public FileExplorerSampleEFCoreDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the FileExplorerSampleDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<FileExplorerSampleDbContext>()
            .Database
            .MigrateAsync();
    }
}
