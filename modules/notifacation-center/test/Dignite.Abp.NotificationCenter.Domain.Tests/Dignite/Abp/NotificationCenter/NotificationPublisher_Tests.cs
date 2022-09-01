using Dignite.Abp.Notifications;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;
using Xunit;

namespace Dignite.Abp.NotificationCenter
{
    public class NotificationPublisher_Tests : NotificationCenterDomainTestBase
    {
        private readonly NotificationPublisher _notificationPublisher;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly ICurrentTenant _currentTenant;
        private readonly INotificationDistributer _notificationDistributer;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IClock _clock;

        public NotificationPublisher_Tests()
        {
            _backgroundJobManager = GetRequiredService<IBackgroundJobManager>();
            _currentTenant = GetRequiredService<ICurrentTenant>();
            _notificationDistributer = GetRequiredService<INotificationDistributer>();
            _guidGenerator = GetRequiredService<IGuidGenerator>();
            _clock = GetRequiredService<IClock>();
            _notificationPublisher = new NotificationPublisher(
                _currentTenant,
                _backgroundJobManager,
                _notificationDistributer,
                _guidGenerator,
                _clock
                );
        }

        [Fact]
        public async Task Should_publish_general_notification()
        {
            //Arrange
            var notificationData = CreateNotificationData();

            //Act
            await _notificationPublisher.PublishAsync("TestNotification", notificationData, severity: NotificationSeverity.Success);

        }

        private static NotificationData CreateNotificationData()
        {
            var notificationData = new NotificationData
            {
                ["TestValue"] = 42
            };

            return notificationData;
        }
    }
}
