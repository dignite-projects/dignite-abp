using Dignite.FileExplorer.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Dignite.FileExplorer;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(FileExplorerEntityFrameworkCoreTestModule)
    )]
public class FileExplorerDomainTestModule : AbpModule
{

}
