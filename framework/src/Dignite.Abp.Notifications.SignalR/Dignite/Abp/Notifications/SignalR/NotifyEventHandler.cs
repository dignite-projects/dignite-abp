using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Dignite.Abp.Notifications.SignalR;

public class NotifyEventHandler : IDistributedEventHandler<RealTimeNotifyEto>, ITransientDependency
{
    private readonly IHubContext<NotificationHub, INotificationClient> _hubContext;

    public NotifyEventHandler(
        IHubContext<NotificationHub, INotificationClient> hubContext
        )
    {
        _hubContext = hubContext;
    }

    public async Task HandleEventAsync(RealTimeNotifyEto eto)
    {
        await _hubContext.Clients.Users(
            eto.UserIds.Select(userId => userId.ToString())
            ).ReceiveNotifications();
    }
}