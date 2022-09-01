using Dignite.Abp.Notifications;
using JetBrains.Annotations;
using System;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.NotificationCenter
{
    public class NotificationSubscription: BasicAggregateRoot, IHasCreationTime, IMultiTenant
    {
        public NotificationSubscription()
        { }


        public NotificationSubscription(NotificationSubscriptionInfo subscription)
            : this(subscription.UserId,
                subscription.NotificationName,
                subscription.EntityTypeName,
                subscription.EntityId,
                subscription.CreationTime,
                subscription.TenantId)
        {
        }

        public NotificationSubscription(Guid userId, string notificationName, string entityTypeName, string entityId, DateTime creationTime, Guid? tenantId)
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
        [NotNull]
        public Guid UserId { get; set; }

        /// <summary>
        /// Notification unique name.
        /// </summary>
        [NotNull]
        public string NotificationName { get; set; }


        /// <summary>
        /// Name of the entity type (including namespaces).
        /// </summary>
        [CanBeNull]
        public string EntityTypeName { get; set; }

        /// <summary>
        /// Entity Id.
        /// </summary>
        [CanBeNull]
        public string EntityId { get; set; }

        /// <summary>
        /// Creation time
        /// </summary>
        public DateTime CreationTime { get; set; }

        public Guid? TenantId { get; set; }

        public override object[] GetKeys()
        {
            return new object[] {
                UserId,
                NotificationName,
                EntityTypeName,
                EntityId
            };
        }
    }
}