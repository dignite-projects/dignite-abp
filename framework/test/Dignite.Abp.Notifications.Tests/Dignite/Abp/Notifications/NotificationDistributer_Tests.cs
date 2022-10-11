using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Dignite.Abp.Notifications;
public class NotificationDistributer_Tests : NotificationsTestBase
{
    private readonly INotificationPublisher _publisher;
    private readonly FakeNotificationDistributer _fakeNotificationDistributer;

    public NotificationDistributer_Tests()
    {
        _publisher = GetRequiredService<INotificationPublisher>();
        _fakeNotificationDistributer = (FakeNotificationDistributer)GetRequiredService<INotificationDistributer>();
    }

    [Fact]
    public async Task Should_Distribute_Notification_Using_Custom_Distributer()
    {
        //Arrange
        var notificationData = new MessageNotificationData("NotificationMessage");

        //Act
        await _publisher.PublishAsync("TestNotification", notificationData,
            severity: NotificationSeverity.Success,
            userIds: new Guid[] {
                Guid.Parse("1a29d843-8924-a0b7-2cc0-3a0487e63926")
            });

        //Assert
        _fakeNotificationDistributer.IsDistributeCalled.ShouldBeTrue();
    }
}
