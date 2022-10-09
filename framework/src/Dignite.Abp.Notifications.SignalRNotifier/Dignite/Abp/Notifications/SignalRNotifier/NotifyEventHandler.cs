using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Dignite.Abp.Notifications.SignalRNotifier;

public class NotifyEventHandler : IDistributedEventHandler<RealTimeNotifyEto>, ITransientDependency
{
    private readonly IHubContext<NotificationsHub, INotificationClient> _hubContext;

    public NotifyEventHandler(
        IHubContext<NotificationsHub, INotificationClient> hubContext
        )
    {
        _hubContext = hubContext;
    }

    public async Task HandleEventAsync(RealTimeNotifyEto notification)
    {
        await _hubContext.Clients.Users(
            notification.UserIds.Select(userId => userId.ToString())
            ).ReceiveNotifications(notification);
    }
}