using System.Collections.Generic;
using System.Linq;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Notifications.Components;
public class NotificationNavigateHandlerSelector:INotificationNavigateHandlerSelector, ITransientDependency
{
    private readonly IEnumerable<INotificationNavigateHandler> _notificationNavigateHandler;
    public NotificationNavigateHandlerSelector(IEnumerable<INotificationNavigateHandler> notificationNavigateHandler)
    {
        _notificationNavigateHandler = notificationNavigateHandler;
    }

    public INotificationNavigateHandler Get(string notificationName)
    {
        var handler = _notificationNavigateHandler.FirstOrDefault(handler => handler.NotificationName == notificationName);

        return handler;
    }
}
