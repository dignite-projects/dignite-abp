using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Notifications
{
    /// <summary>
    /// This background job distributes notifications to users.
    /// </summary>
    public class NotificationDistributionJob : AsyncBackgroundJob<NotificationDistributionJobArgs>, ITransientDependency
    {
        private readonly INotificationDistributer _notificationDistributer;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationDistributionJob"/> class.
        /// </summary>
        public NotificationDistributionJob(
            INotificationDistributer notificationDistributer)
        {
            _notificationDistributer = notificationDistributer;
        }

        public override async Task ExecuteAsync(NotificationDistributionJobArgs args)
        {
            await _notificationDistributer.DistributeAsync(args.Notification,args.UserIds,args.ExcludedUserIds);
        }
    }
}
