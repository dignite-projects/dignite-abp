namespace Dignite.Abp.Notifications.Components;

/// <summary>
/// Jumps for processing notifications
/// </summary>
public interface INotificationNavigateHandler
{
    /// <summary>
    /// 
    /// </summary>
    string NotificationName { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns>Navigation link back to notification</returns>
    void Excute(NotificationNavigationContext context);
}
