using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dignite.Abp.Notifications
{
    /// <summary>
    /// Used to manage subscriptions for notifications.
    /// </summary>
    public interface INotificationSubscriptionManager
    {
        /// <summary>
        /// Subscribes to a notification for given user and notification informations.
        /// </summary>
        /// <param name="userId">User</param>
        /// <param name="notificationName">Name of the notification.</param>
        /// <param name="entityIdentifier">entity identifier</param>
        Task SubscribeAsync([NotNull] Guid userId, [NotNull]string notificationName, [CanBeNull]NotificationEntityIdentifier entityIdentifier = null);

        /// <summary>
        /// Subscribes to all available notifications for current user.
        /// It does not subscribe entity related notifications.
        /// </summary>
        Task SubscribeToAllAvailableNotificationsAsync([NotNull] Guid userId);

        /// <summary>
        /// Unsubscribes from a notification.
        /// </summary>
        /// <param name="userId">User.</param>
        /// <param name="notificationName">Name of the notification.</param>
        /// <param name="entityIdentifier">entity identifier</param>
        Task UnsubscribeAsync([NotNull] Guid userId, [NotNull] string notificationName, [CanBeNull] NotificationEntityIdentifier entityIdentifier = null);

        /// <summary>
        /// Gets all subscribtions for given notification (including all tenants).
        /// This only works for single database approach in a multitenant application!
        /// </summary>
        /// <param name="notificationName">Name of the notification.</param>
        /// <param name="entityIdentifier">entity identifier</param>
        Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync([NotNull] string notificationName, [CanBeNull] NotificationEntityIdentifier entityIdentifier = null);

        /// <summary>
        /// Gets subscribed notifications for a user.
        /// </summary>
        /// <param name="userId">User.</param>
        Task<List<NotificationSubscriptionInfo>> GetSubscribedNotificationsAsync([NotNull] Guid userId);

        /// <summary>
        /// Checks if a user subscribed for a notification.
        /// </summary>
        /// <param name="userId">User.</param>
        /// <param name="notificationName">Name of the notification.</param>
        /// <param name="entityIdentifier">entity identifier</param>
        Task<bool> IsSubscribedAsync([NotNull] Guid userId, [NotNull] string notificationName, [CanBeNull] NotificationEntityIdentifier entityIdentifier = null);
    }
}
