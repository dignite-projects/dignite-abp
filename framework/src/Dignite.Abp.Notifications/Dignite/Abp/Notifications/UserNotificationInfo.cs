using System;

namespace Dignite.Abp.Notifications;

/// <summary>
/// Used to store a user notification.
/// </summary>
public class UserNotificationInfo
{
    public UserNotificationInfo(Guid userId, Guid notificationId, UserNotificationState state, Guid? tenantId)
    {
        UserId = userId;
        NotificationId = notificationId;
        State = state;
        TenantId = tenantId;
    }

    /// <summary>
    /// User Id.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Notification Id.
    /// </summary>
    public Guid NotificationId { get; set; }

    /// <summary>
    /// Current state of the user notification.
    /// </summary>
    public UserNotificationState State { get; set; }

    public Guid? TenantId { get; set; }
}