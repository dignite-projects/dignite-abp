using System.Collections.Generic;

namespace Dignite.Abp.Notifications
{
    /// <summary>
    /// Used as a context while defining notifications.
    /// </summary>
    public interface INotificationDefinitionContext
    {
        NotificationDefinition GetOrNull(string name);

        IReadOnlyList<NotificationDefinition> GetAll();

        void Add(params NotificationDefinition[] definitions);
    }
}