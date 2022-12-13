using System;

namespace Dignite.Abp.Notifications.Components;
public class NotificationNavigationContext
{
    public Guid UserId { get; set; }

    public Guid NotificationId { get; set; }

    /// <summary>
    /// Unique notification name.
    /// </summary>
    public string NotificationName { get; set; }

    /// <summary>
    /// Can be used to add custom properties to this notification.
    /// </summary>
    public NotificationData Data { get; set; }

    /// <summary>
    /// Gets/sets entity type name, if this is an entity level notification.
    /// It's FullName of the entity type.
    /// </summary>
    public string EntityTypeName { get; set; }

    /// <summary>
    /// Gets/sets primary key of the entity, if this is an entity level notification.
    /// </summary>
    public string EntityId { get; set; }
}
