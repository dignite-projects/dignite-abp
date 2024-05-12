using test_Item.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace test_Item.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(test_ItemEntityFrameworkCoreModule),
    typeof(test_ItemApplicationContractsModule)
)]
public class test_ItemDbMigratorModule : AbpModule
{
}
