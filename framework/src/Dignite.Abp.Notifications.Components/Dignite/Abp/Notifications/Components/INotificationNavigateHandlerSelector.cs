namespace Dignite.Abp.Notifications.Components;
public interface INotificationNavigateHandlerSelector
{
    /// <summary>
    /// Get notification navigation handler using notification name
    /// </summary>
    /// <param name="notificationName">
    /// </param>
    /// <returns></returns>
    INotificationNavigateHandler Get(string notificationName);
}
