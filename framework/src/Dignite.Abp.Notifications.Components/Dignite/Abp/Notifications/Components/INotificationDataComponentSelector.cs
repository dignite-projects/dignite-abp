using System;
using JetBrains.Annotations;

namespace Dignite.Abp.Notifications.Components;

public interface INotificationDataComponentSelector
{
    /// <summary>
    /// Get blazor component using field control provider name
    /// </summary>
    /// <param name="notificationDataTypeFullName">
    /// <see cref="UserNotificationDto.Data"/>
    /// </param>
    /// <returns></returns>
    [NotNull]
    INotificationDataComponent Get(string notificationDataTypeFullName);
}

