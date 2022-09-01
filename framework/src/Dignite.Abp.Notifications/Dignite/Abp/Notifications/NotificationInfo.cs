using System;

namespace Dignite.Abp.Notifications
{
    /// <summary>
    /// Used to store a notification request.
    /// </summary>
    public class NotificationInfo
    {
        public NotificationInfo(Guid id, string notificationName, NotificationData data, string entityTypeName, string entityId, NotificationSeverity severity, DateTime creationTime,Guid? tenantId)
        {
            Id = id;
            NotificationName = notificationName;
            Data = data;
            EntityTypeName = entityTypeName;
            EntityId = entityId;
            Severity = severity;
            CreationTime = creationTime;
            TenantId = tenantId;
        }

        public Guid Id { get; set; }

        /// <summary>
        /// Unique notification name.
        /// </summary>
        public string NotificationName { get; set; }


        /// <summary>
        /// Can be used to add custom properties to this notification.
        /// </summary>
        public NotificationData Data { get; set; }

        /// <summary>
        /// Gets/sets entity type name, if this is an entity level notification.
        /// It's FullName of the entity type.
        /// </summary>
        public string EntityTypeName { get; set; }


        /// <summary>
        /// Gets/sets primary key of the entity, if this is an entity level notification.
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// Notification severity.
        /// </summary>
        public NotificationSeverity Severity { get; set; }

        /// <summary>
        /// Creation time
        /// </summary>
        public DateTime CreationTime { get; set; }

        public Guid? TenantId { get; set; }
    }
}