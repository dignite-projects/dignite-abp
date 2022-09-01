using Dignite.Abp.NotificationCenter.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Dignite.Abp.NotificationCenter
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(AbpBackgroundJobsModule),
        typeof(NotificationCenterEntityFrameworkCoreTestModule)
        )]
    public class NotificationCenterDomainTestModule : AbpModule
    {
        
    }
}
