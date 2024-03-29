﻿namespace Dignite.Abp.Notifications;

/// <summary>
/// A class contains a <see cref="UserNotificationInfo"/> and related <see cref="NotificationInfo"/>.
/// </summary>
public class UserNotificationWithNotification
{
    /// <summary>
    /// User notification.
    /// </summary>
    public UserNotificationInfo UserNotification { get; set; }

    /// <summary>
    /// Notification.
    /// </summary>
    public NotificationInfo Notification { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserNotificationWithNotification"/> class.
    /// </summary>
    public UserNotificationWithNotification(UserNotificationInfo userNotification, NotificationInfo notification)
    {
        UserNotification = userNotification;
        Notification = notification;
    }
}