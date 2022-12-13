using System;
namespace Dignite.Abp.Notifications.Components;

/// <summary>
/// Used to notification the data component interface
/// </summary>
public interface INotificationDataComponent
{
    Type NotificationDataType { get; }
}

