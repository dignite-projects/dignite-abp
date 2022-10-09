using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.NotificationCenter;

public class NotificationSubscriptionDto
{
    public NotificationSubscriptionDto(string notificationName, string displayName, string description, bool isSubscribed)
    {
        NotificationName = notificationName;
        DisplayName = displayName;
        Description = description;
        IsSubscribed = isSubscribed;
    }

    /// <summary>
    /// Notification unique name.
    /// </summary>
    public string NotificationName { get; set; }

    /// <summary>
    /// Display name of the notification.
    /// Optional.
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// Description for the notification.
    /// Optional.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool IsSubscribed { get; set; }
}