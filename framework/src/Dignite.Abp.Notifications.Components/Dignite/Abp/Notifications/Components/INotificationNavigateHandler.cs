namespace Dignite.Abp.Notifications.Components;
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
