using Microsoft.AspNetCore.Components;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Notifications.Components;
public abstract class NotificationNavigateHandlerBase : INotificationNavigateHandler, ITransientDependency
{
    public NavigationManager NavigationManager { get; set; }

    public abstract string NotificationName { get; }

    public abstract void Excute(NotificationNavigationContext context);
}
