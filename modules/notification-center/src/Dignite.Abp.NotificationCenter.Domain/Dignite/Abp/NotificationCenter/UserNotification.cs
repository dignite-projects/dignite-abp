using System;
using Dignite.Abp.Notifications;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.NotificationCenter;

public class UserNotification : BasicAggregateRoot<Guid>, IHasCreationTime, IMultiTenant
{
    public UserNotification()
    {
    }

    public UserNotification(Guid id, UserNotificationInfo userNotification)
    : this(
          id,
         userNotification.UserId,
         userNotification.NotificationId,
         userNotification.State,
         userNotification.TenantId)
    {
    }

    public UserNotification(Guid id, Guid userId, Guid notificationId, UserNotificationState state, Guid? tenantId)
        :base(id)
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

    /// <summary>
    /// Creation time
    /// </summary>
    public DateTime CreationTime { get; set; }

    public Guid? TenantId { get; set; }

}