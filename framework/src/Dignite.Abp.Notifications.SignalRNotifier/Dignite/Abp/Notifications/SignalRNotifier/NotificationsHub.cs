using Volo.Abp.AspNetCore.SignalR;

namespace Dignite.Abp.Notifications.SignalRNotifier;

public class NotificationsHub : AbpHub<INotificationClient>
{
}