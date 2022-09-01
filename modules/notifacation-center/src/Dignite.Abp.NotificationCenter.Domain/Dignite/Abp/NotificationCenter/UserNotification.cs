using Dignite.Abp.Notifications;
using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.NotificationCenter
{
    public class UserNotification : BasicAggregateRoot, IMultiTenant
    {
        public UserNotification()
        {
        }
        public UserNotification(UserNotificationInfo userNotification)
        :this(
             userNotification.UserId,
             userNotification.NotificationId,
             userNotification.State,
             userNotification.TenantId)
        {
        }

        public UserNotification(Guid userId, Guid notificationId, UserNotificationState state, Guid? tenantId)
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
        /// 
        /// </summary>
        public Notification Notification { get; set; }

        public Guid? TenantId { get; set; }

        public override object[] GetKeys()
        {
            return new object[] {
                UserId,NotificationId
            };
        }
    }
}
