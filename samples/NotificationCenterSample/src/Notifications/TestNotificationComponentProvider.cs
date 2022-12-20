using Dignite.Abp.Notifications.Components;

namespace NotificationCenterSample.Notifications;

public class TestNotificationComponentProvider : INotificationComponentProvider
{
    public string NotificationName => NotificationCenterSampleNotifications.TestNotification;

    public string GetIcon(string entityId = null)
    {
        return "fa fa-asterisk";
    }
}
