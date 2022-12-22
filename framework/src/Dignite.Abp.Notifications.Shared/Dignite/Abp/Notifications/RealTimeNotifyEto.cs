using System;
using Volo.Abp.EventBus;

namespace Dignite.Abp.Notifications;

[EventName("Dignite.Abp.Notifications.RealTimeNotify")]
public class RealTimeNotifyEto
{
    public RealTimeNotifyEto(Guid notificationId, string notificationName, string notificationDisplayName, NotificationData data, NotificationSeverity severity, DateTime creationTime, Guid[] userIds)
    {
        NotificationId = notificationId;
        NotificationName = notificationName;
        NotificationDisplayName = notificationDisplayName;
        Data = data;
        Severity = severity;
        CreationTime = creationTime;
        UserIds = userIds;
    }


    /// <summary>
    /// Notification Id.
    /// </summary>
    public Guid NotificationId { get; set; }

    /// <summary>
    /// Unique notification name.
    /// </summary>
    public string NotificationName { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string NotificationDisplayName { get; set; }

    /// <summary>
    /// Can be used to add custom properties to this notification.
    /// </summary>
    public NotificationData Data { get; set; }

    /// <summary>
    /// Notification severity.
    /// </summary>
    public NotificationSeverity Severity { get; set; }

    /// <summary>
    /// Creation time
    /// </summary>
    public DateTime CreationTime { get; set; }

    public Guid[] UserIds { get; set; }
}