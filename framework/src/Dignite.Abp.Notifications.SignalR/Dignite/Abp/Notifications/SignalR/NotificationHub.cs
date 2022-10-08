using Volo.Abp.AspNetCore.SignalR;

namespace Dignite.Abp.Notifications.SignalR;

public class NotificationHub : AbpHub<INotificationClient>
{
}