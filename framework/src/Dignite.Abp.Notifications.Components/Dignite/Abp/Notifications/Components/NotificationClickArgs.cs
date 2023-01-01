namespace Dignite.Abp.Notifications.Components;
public class NotificationClickArgs
{
    public NotificationClickArgs(NotificationData data, string entityTypeName, string entityId)
    {
        Data = data;
        EntityTypeName = entityTypeName;
        EntityId = entityId;
    }

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
