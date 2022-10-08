using System.Threading.Tasks;

namespace Dignite.Abp.Notifications.SignalR;

public interface INotificationClient
{
    Task ReceiveNotifications(RealTimeNotifyEto notification);
}