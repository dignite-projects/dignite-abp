using System;
using Volo.Abp.BackgroundJobs;

namespace Dignite.Abp.Notifications
{
    /// <summary>
    /// Arguments for <see cref="NotificationDistributionJob"/>
    /// </summary>
    [Serializable]
    [BackgroundJobName("notifications")]
    public class NotificationDistributionJobArgs
    {
        /// <summary>
        /// Notification Info.
        /// </summary>
        public NotificationInfo Notification { get; set; }


        public Guid[] UserIds { get; set; }
        public Guid[] ExcludedUserIds { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationDistributionJobArgs"/> class.
        /// </summary>
        public NotificationDistributionJobArgs(
            NotificationInfo notification,
            Guid[] userIds = null,
            Guid[] excludedUserIds = null            
            )
        {
            Notification = notification;
            UserIds = userIds;
            ExcludedUserIds = excludedUserIds;
        }
    }
}