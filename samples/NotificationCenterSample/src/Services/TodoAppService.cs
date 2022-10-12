using Dignite.Abp.Notifications;
using Volo.Abp.Application.Services;

namespace NotificationCenterSample.Services
{
    public class TodoAppService : ApplicationService
    {
        private readonly INotificationPublisher _publisher;

        public TodoAppService(INotificationPublisher notificationPublisher)
        {
            _publisher = notificationPublisher;
        }


        public async Task<NotificationData> CreateAsync(string text)
        {
            //Arrange
            var notificationData = new MessageNotificationData(text);

            //Act
            await _publisher.PublishAsync("TestNotification", notificationData,
                severity: NotificationSeverity.Success,
                userIds: new Guid[] {
                    CurrentUser.Id.Value
                    }
                );

            return notificationData;
        }

    }
}

