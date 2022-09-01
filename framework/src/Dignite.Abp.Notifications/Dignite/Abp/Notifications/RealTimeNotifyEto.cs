using Volo.Abp.EventBus;

namespace Dignite.Abp.Notifications
{
    [EventName("Dignite.Abp.Notifications.RealTimeNotify")]
    public class RealTimeNotifyEto
    {

        public RealTimeNotifyEto(NotificationInfo notificationInfo, UserNotificationInfo[] userNotifications)
        {
            NotificationInfo = notificationInfo;
            UserNotifications = userNotifications;
        }

        public NotificationInfo NotificationInfo { get; set; }
        public UserNotificationInfo[] UserNotifications { get; set; }
    }
}
