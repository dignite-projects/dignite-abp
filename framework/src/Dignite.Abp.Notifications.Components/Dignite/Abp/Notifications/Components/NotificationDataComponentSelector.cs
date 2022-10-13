using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Notifications.Components;

public class NotificationDataComponentSelector:INotificationDataComponentSelector, ITransientDependency
{
    private readonly IEnumerable<INotificationDataComponent> _notificationDataComponent;
    public NotificationDataComponentSelector(IEnumerable<INotificationDataComponent> notificationDataComponent)
    {
        _notificationDataComponent = notificationDataComponent;
    }

    /// <summary>
    /// Get blazor component using field control provider name
    /// </summary>
    /// <param name="notificationDataTypeFullName">
    /// <see cref="UserNotificationDto.Data"/>
    /// </param>
    /// <returns></returns>
    [NotNull]
    public INotificationDataComponent Get(string notificationDataTypeFullName)
    {
        var notificationDataComponent = _notificationDataComponent.FirstOrDefault(ndc => ndc.NotificationDataType.FullName == notificationDataTypeFullName);

        if (notificationDataComponent == null)
            throw new AbpException(
                $"Could not find the notification data component with the notification data type full name ({notificationDataTypeFullName}) ."
            );
        else
            return notificationDataComponent;
    }
}

