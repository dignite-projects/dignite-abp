using System;
namespace Dignite.Abp.Notifications.Components;

/// <summary>
/// Used to notification the data component interface
/// </summary>
public interface INotificationIconComponent
{
    /// <summary>
    /// Notification Definition Name
    /// </summary>
    string NotificationName { get; }
}

