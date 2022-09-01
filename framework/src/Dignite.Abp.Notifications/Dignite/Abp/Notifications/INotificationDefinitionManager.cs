using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dignite.Abp.Notifications
{
    /// <summary>
    /// Used to manage notification definitions.
    /// </summary>
    public interface INotificationDefinitionManager
    {

        /// <summary>
        /// Gets a notification definition by name.
        /// Throws exception if there is no notification definition with given name.
        /// </summary>
        NotificationDefinition Get(string name);

        /// <summary>
        /// Gets a notification definition by name.
        /// Returns null if there is no notification definition with given name.
        /// </summary>
        NotificationDefinition GetOrNull(string name);

        /// <summary>
        /// Gets all notification definitions.
        /// </summary>
        IReadOnlyList<NotificationDefinition> GetAll();

        /// <summary>
        /// Checks if given notification (<paramref name="name"/>) is available for given user.
        /// </summary>
        Task<bool> IsAvailableAsync(string name, Guid userId);

        /// <summary>
        /// Gets all available notification definitions for given user.
        /// </summary>
        Task<IReadOnlyList<NotificationDefinition>> GetAllAvailableAsync(Guid userId);
    }
}