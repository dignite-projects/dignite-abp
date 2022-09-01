using System;
using System.Threading.Tasks;

namespace Dignite.Abp.Notifications
{
    /// <summary>
    /// Used to distribute notifications to users.
    /// </summary>
    public interface INotificationDistributer
    {
        /// <summary>
        /// Distributes given notification to users.
        /// </summary>
        /// <param name="notification">The notification info.</param>
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
        Task DistributeAsync(
            NotificationInfo notification,
            Guid[] userIds = null,
            Guid[] excludedUserIds = null);
    }
}
