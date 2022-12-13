using JetBrains.Annotations;

namespace Dignite.Abp.Notifications.Components;

public interface INotificationDataComponentSelector
{
    /// <summary>
    /// Get blazor component using NotificationDataTypeFullName
    /// </summary>
    /// <param name="notificationDataTypeFullName">
    /// <see cref="NotificationData.Type"/>
    /// </param>
    /// <returns></returns>
    [NotNull]
    INotificationDataComponent Get(string notificationDataTypeFullName);
}

