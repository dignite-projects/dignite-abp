using Dignite.Abp.Notifications;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Dignite.Abp.AspNetCore.SignalR.Notifications
{
    public class NotifyEventHandler : IDistributedEventHandler<RealTimeNotifyEto>, ITransientDependency
    {

        private readonly IHubContext<NotificationHub,INotificationClient> _hubContext;

        public NotifyEventHandler(
        IHubContext<NotificationHub, INotificationClient> hubContext)
        {
            _hubContext = hubContext;
        }


        public async Task HandleEventAsync(RealTimeNotifyEto eto)
        {
            await _hubContext.Clients.Users(
                eto.UserNotifications.Select(un => un.UserId.ToString())
                ).ReceiveNotifications();
            //await _hubContext.Clients.All.ReceiveNotifications();
        }
    }
}
