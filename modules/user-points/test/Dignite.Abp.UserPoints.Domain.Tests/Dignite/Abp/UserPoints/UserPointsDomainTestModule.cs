using Dignite.Abp.UserPoints.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Dignite.Abp.UserPoints;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(UserPointsEntityFrameworkCoreTestModule)
    )]
public class UserPointsDomainTestModule : AbpModule
{

}
