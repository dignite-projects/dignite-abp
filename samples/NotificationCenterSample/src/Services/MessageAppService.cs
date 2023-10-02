using Dignite.Abp.Notifications;
using NotificationCenterSample.Notifications;
using Volo.Abp.Application.Services;

namespace NotificationCenterSample.Services;

public class MessageAppService : ApplicationService
{
    private readonly INotificationPublisher _publisher;

    public MessageAppService(INotificationPublisher notificationPublisher)
    {
        _publisher = notificationPublisher;
    }


    public async Task<NotificationData> CreateAsync(string text)
    {
        var notificationData = new MessageNotificationData(text);

        //Publish
        await _publisher.PublishAsync(
            NotificationCenterSampleNotifications.TestNotification, 
            notificationData,
            severity: NotificationSeverity.Success,
            userIds: new Guid[] {
                CurrentUser.Id.Value
                }
            );

        return notificationData;
    }

}

