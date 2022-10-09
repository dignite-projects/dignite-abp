using System.Threading.Tasks;

namespace Dignite.Abp.Notifications.SignalRNotifier;

public interface INotificationClient
{
    Task ReceiveNotifications(RealTimeNotifyEto notification);
}