using System;

namespace Dignite.Abp.Notifications
{
    public class NotificationEntityIdentifier
    {
        /// <summary>
        /// Entity Type.
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Entity's Id.
        /// </summary>
        public string Id { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationEntityIdentifier"/> class.
        /// </summary>
        /// <param name="type">Entity type.</param>
        /// <param name="id">Id of the entity.</param>
        public NotificationEntityIdentifier(Type type, string id)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            Type = type;
            Id = id;
        }
    }
}
