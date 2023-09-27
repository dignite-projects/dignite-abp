using System;
using Dignite.Abp.NotificationCenter.EntityFrameworkCore;

namespace Dignite.Abp.NotificationCenter;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */

public abstract class NotificationCenterApplicationTestBase : NotificationCenterTestBase<DigniteAbpNotificationCenterApplicationTestModule>
{
    protected virtual void UsingDbContext(Action<INotificationCenterDbContext> action)
    {
        using (var dbContext = GetRequiredService<INotificationCenterDbContext>())
        {
            action.Invoke(dbContext);
        }
    }

    protected virtual T UsingDbContext<T>(Func<INotificationCenterDbContext, T> action)
    {
        using (var dbContext = GetRequiredService<INotificationCenterDbContext>())
        {
            return action.Invoke(dbContext);
        }
    }
}