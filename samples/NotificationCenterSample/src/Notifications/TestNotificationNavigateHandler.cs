using Dignite.Abp.Notifications.Components;

namespace NotificationCenterSample.Notifications;

public class TestNotificationNavigateHandler : NotificationNavigateHandlerBase
{
    public override string NotificationName => NotificationCenterSampleNotifications.TestNotification;


    public override void Excute(NotificationNavigationContext context)
    {
        NavigationManager.NavigateTo("/TestNotifications");
    }
}
