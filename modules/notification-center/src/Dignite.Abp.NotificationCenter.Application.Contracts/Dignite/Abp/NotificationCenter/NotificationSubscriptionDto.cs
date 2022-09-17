using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;

namespace Dignite.Abp.NotificationCenter;

public class NotificationSubscriptionDto:EntityDto<Guid>
{
    /// <summary>
    /// User Id.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Notification unique name.
    /// </summary>
    public string NotificationName { get; set; }

    /// <summary>
    /// Creation time
    /// </summary>
    public DateTime CreationTime { get; set; }

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
}