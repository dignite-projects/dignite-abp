using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using test_Item.Data;
using Volo.Abp.DependencyInjection;

namespace test_Item.EntityFrameworkCore;

public class EntityFrameworkCoretest_ItemDbSchemaMigrator
    : Itest_ItemDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoretest_ItemDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the test_ItemDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<test_ItemDbContext>()
            .Database
            .MigrateAsync();
    }
}
