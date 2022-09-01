using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Dignite.Abp.Notifications
{
    internal class NotificationDefinitionContext : INotificationDefinitionContext
    {
        protected Dictionary<string, NotificationDefinition> Notifications { get; }

        public NotificationDefinitionContext(Dictionary<string, NotificationDefinition> notifications)
        {
            Notifications = notifications;
        }

        public virtual NotificationDefinition GetOrNull(string name)
        {
            return Notifications.GetOrDefault(name);
        }

        public virtual IReadOnlyList<NotificationDefinition> GetAll()
        {
            return Notifications.Values.ToImmutableList();
        }

        public virtual void Add(params NotificationDefinition[] definitions)
        {
            if (definitions.IsNullOrEmpty())
            {
                return;
            }

            foreach (var definition in definitions)
            {
                Notifications[definition.Name] = definition;
            }
        }
    }
}
