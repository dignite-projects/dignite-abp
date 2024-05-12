using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace test_Item.Data;

/* This is used if database provider does't define
 * Itest_ItemDbSchemaMigrator implementation.
 */
public class Nulltest_ItemDbSchemaMigrator : Itest_ItemDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
