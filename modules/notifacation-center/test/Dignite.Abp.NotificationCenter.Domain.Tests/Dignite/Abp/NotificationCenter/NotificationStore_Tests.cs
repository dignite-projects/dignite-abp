using System.Threading.Tasks;
using Dignite.Abp.Notifications;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;
using Xunit;
using Shouldly;
using Volo.Abp.Uow;

namespace Dignite.Abp.NotificationCenter;

public class NotificationStore_Tests : NotificationCenterDomainTestBase
{
    private readonly NotificationPublisher _notificationPublisher;
    private readonly IBackgroundJobManager _backgroundJobManager;
    private readonly ICurrentTenant _currentTenant;
    private readonly INotificationDistributer _notificationDistributer;
    private readonly IGuidGenerator _guidGenerator;
    private readonly IClock _clock;
    private readonly NotificationCenterTestData _notificationCenterTestData;
    private readonly IUserNotificationManager _userNotificationManager;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public NotificationStore_Tests()
    {
        _backgroundJobManager = GetRequiredService<IBackgroundJobManager>();
        _currentTenant = GetRequiredService<ICurrentTenant>();
        _notificationDistributer = GetRequiredService<INotificationDistributer>();
        _guidGenerator = GetRequiredService<IGuidGenerator>();
        _clock = GetRequiredService<IClock>();
        _notificationCenterTestData = GetRequiredService<NotificationCenterTestData>();
        _userNotificationManager = GetRequiredService<IUserNotificationManager>();
        _unitOfWorkManager = GetRequiredService<IUnitOfWorkManager>();
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
        await _notificationPublisher.PublishAsync(
            "FakeDefinitionNotification",
            notificationData,
            severity: NotificationSeverity.Success,
            userIds:new System.Guid[] { _notificationCenterTestData .User1Id}
            );

        var userNotifications = await _userNotificationManager.GetUserNotificationsAsync(_notificationCenterTestData.User1Id);
        userNotifications.ShouldNotBeEmpty();
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