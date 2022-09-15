using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Notifications;
public class FakeNotificationDistributer : INotificationDistributer,ISingletonDependency
{
    public bool IsDistributeCalled { get; set; }

    public Task DistributeAsync(NotificationInfo notification, Guid[] userIds = null, Guid[] excludedUserIds = null)
    {
        IsDistributeCalled = true;
        return Task.CompletedTask;
    }
}
