using Dignite.Abp.Notifications;
using System;
using System.Text.Json;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.NotificationCenter
{
    public class Notification : BasicAggregateRoot<Guid>, IHasCreationTime, IMultiTenant
    {
        public Notification()
        {
        }

        public Notification(NotificationInfo notification)
            :this(
                 notification.Id,
                 notification.NotificationName,
                 notification.Data,
                 notification.EntityTypeName,
                 notification.EntityId,
                 notification.Severity,
                 notification.CreationTime,
                 notification.TenantId)
        {
        }

        public Notification(Guid id, string notificationName, NotificationData data, string entityTypeName, string entityId, NotificationSeverity severity, DateTime creationTime, Guid? tenantId)
        {
            Id= id;
            NotificationName = notificationName;
            Data = JsonSerializer.Serialize(data);
            DataTypeName = data?.GetType().AssemblyQualifiedName;
            EntityTypeName = entityTypeName;
            EntityId = entityId;
            Severity = severity;
            CreationTime = creationTime;
            TenantId = tenantId;
        }

        /// <summary>
        /// Unique notification name.
        /// </summary>
        public string NotificationName { get; set; }


        /// <summary>
        /// Notification data  as JSON string.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Type of the JSON serialized <see cref="Data"/>.
        /// It's FullName of the entity type.
        /// </summary>
        public string DataTypeName { get; set; }

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
