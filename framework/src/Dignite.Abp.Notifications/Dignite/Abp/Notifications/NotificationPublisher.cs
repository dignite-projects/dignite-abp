using JetBrains.Annotations;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Security.Claims;
using Volo.Abp.Timing;
using Volo.Abp.Users;

namespace Dignite.Abp.Notifications
{
    /// <summary>
    /// Implements <see cref="INotificationPublisher"/>.
    /// </summary>
    public class NotificationPublisher : INotificationPublisher, ITransientDependency
    {
        public const int MaxUserCountToDirectlyDistributeANotification = 5;

        private readonly ICurrentTenant _currentTenant;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly INotificationDistributer _notificationDistributer;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IClock _clock;

        public NotificationPublisher(ICurrentTenant currentTenant, IBackgroundJobManager backgroundJobManager, INotificationDistributer notificationDistributer, IGuidGenerator guidGenerator, IClock clock)
        {
            _currentTenant = currentTenant;
            _backgroundJobManager = backgroundJobManager;
            _notificationDistributer = notificationDistributer;
            _guidGenerator = guidGenerator;
            _clock = clock;
        }

        public virtual async Task PublishAsync(
            [NotNull]string notificationName,
            NotificationData data = null,
            NotificationEntityIdentifier entityIdentifier = null,
            NotificationSeverity severity = NotificationSeverity.Info,
            Guid[] userIds = null,
            Guid[] excludedUserIds = null)
        {
            var notificationInfo = new NotificationInfo(
                _guidGenerator.Create(),            
                notificationName,
                data,
                entityIdentifier?.Type.FullName,
                entityIdentifier?.Id,
                severity,
                _clock.Now,
                _currentTenant.Id
            );


            if (userIds != null && userIds.Length <= MaxUserCountToDirectlyDistributeANotification)
            {
                //We can directly distribute the notification since there are not much receivers
                await _notificationDistributer.DistributeAsync(notificationInfo, userIds, excludedUserIds);
            }
            else
            {
                //We enqueue a background job since distributing may get a long time
                await _backgroundJobManager.EnqueueAsync(
                    new NotificationDistributionJobArgs(
                        notificationInfo,
                        userIds,
                        excludedUserIds
                        )
                );
            }
        }
    }
}
