using JetBrains.Annotations;
using System;
using System.Threading.Tasks;

namespace Dignite.Abp.Notifications
{
    /// <summary>
    /// Used to publish notifications.
    /// </summary>
    public interface INotificationPublisher
    {
        /// <summary>
        /// Publishes a new notification.
        /// </summary>
        /// <param name="notificationName">Unique notification name</param>
        /// <param name="data">Notification data (optional)</param>
        /// <param name="entityIdentifier">The entity identifier if this notification is related to an entity</param>
        /// <param name="severity">Notification severity</param>
        /// <param name="userIds">
        /// Target user id(s). 
        /// Used to send notification to specific user(s) (without checking the subscription). 
        /// If this is null/empty, the notification is sent to subscribed users.
        /// </param>
        /// <param name="excludedUserIds">
        /// Excluded user id(s).
        /// This can be set to exclude some users while publishing notifications to subscribed users.
        /// It's normally not set if <paramref name="userIds"/> is set.
        /// </param>
        Task PublishAsync(
            [NotNull] string notificationName,
            NotificationData data = null,
            NotificationEntityIdentifier entityIdentifier = null,
            NotificationSeverity severity = NotificationSeverity.Info,
            Guid[] userIds = null,
            Guid[] excludedUserIds = null);
    }
}
