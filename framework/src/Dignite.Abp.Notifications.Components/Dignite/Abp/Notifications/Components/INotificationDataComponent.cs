using System;
namespace Dignite.Abp.Notifications.Components;

public interface INotificationDataComponent
{
    Type NotificationDataType { get; }
}

