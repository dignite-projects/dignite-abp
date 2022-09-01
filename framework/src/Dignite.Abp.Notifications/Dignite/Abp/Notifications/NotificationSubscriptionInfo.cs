using System;

namespace Dignite.Abp.Notifications
{
    /// <summary>
    /// Represents a user subscription to a notification.
    /// </summary>
    public class NotificationSubscriptionInfo
    {
        public NotificationSubscriptionInfo(Guid userId, string notificationName, string entityTypeName, string entityId, DateTime creationTime, Guid? tenantId)
        {
            UserId = userId;
            NotificationName = notificationName;
            EntityTypeName = entityTypeName;
            EntityId = entityId;
            CreationTime = creationTime;
            TenantId = tenantId;
        }

        /// <summary>
        /// User Id.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Notification unique name.
        /// </summary>
        public string NotificationName { get; set; }


        /// <summary>
        /// Name of the entity type (including namespaces).
        /// </summary>
        public string EntityTypeName { get; set; }

        /// <summary>
        /// Entity Id.
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// Creation time
        /// </summary>
        public DateTime CreationTime { get; set; }

        public Guid? TenantId { get; set; }
    }
}