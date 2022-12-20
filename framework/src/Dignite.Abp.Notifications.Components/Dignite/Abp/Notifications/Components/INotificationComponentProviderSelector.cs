using JetBrains.Annotations;

namespace Dignite.Abp.Notifications.Components;

/// <summary>
/// Notification Component Selector
/// </summary>
public interface INotificationComponentProviderSelector
{
    /// <summary>
    /// Get blazor component using NotificationDataTypeFullName
    /// </summary>
    /// <param name="notificationName">
    /// </param>
    /// <returns></returns>
    [NotNull]
    INotificationComponentProvider Get(string notificationName);
}

