using Dignite.Abp.Notifications;
using System;

namespace Dignite.Abp.NotificationCenter
{
    public class UserNotificationDto
    {
        public Guid UserId { get; set; }

        public Guid NotificationId { get; set; }

        /// <summary>
        /// Unique notification name.
        /// </summary>
        public string NotificationName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string NotificationDisplayName { get; set; }


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


        /// <summary>
        /// Current state of the user notification.
        /// </summary>
        public UserNotificationState State { get; set; }
    }
}
