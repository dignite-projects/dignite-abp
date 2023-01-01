using Dignite.Abp.Notifications.Components;
using Microsoft.AspNetCore.Components;
using NotificationCenterSample.Pages;

namespace NotificationCenterSample.Notifications;

public class TestNotificationComponentProvider : INotificationComponentProvider
{
    private readonly NavigationManager _navigationManager;

    public TestNotificationComponentProvider(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    public string NotificationName => NotificationCenterSampleNotifications.TestNotification;

    public Type IconComponentType => typeof(TestNotificationIconComponent);

    public Task NotificationClickAsync(NotificationClickArgs args)
    {
        _navigationManager.NavigateTo("/identity/users", false);
        return Task.CompletedTask;
    }
}
