using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dignite.Abp.Notifications
{
    /// <summary>
    /// Used to manage user notifications.
    /// </summary>
    public interface IUserNotificationManager
    {
        /// <summary>
        /// Gets notifications for a user.
        /// </summary>
        /// <param name="userId">User.</param>
        /// <param name="state">State</param>
        /// <param name="skipCount">Skip count.</param>
        /// <param name="maxResultCount">Maximum result count.</param>
        /// <param name="startDate">List notifications published after startDateTime</param>
        /// <param name="endDate">List notifications published before startDateTime</param>
        Task<List<UserNotificationWithNotification>> GetUserNotificationsAsync(Guid userId, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue, DateTime? startDate = null, DateTime? endDate = null);            

        /// <summary>
        /// Gets user notification count.
        /// </summary>
        /// <param name="userId">User.</param>
        /// <param name="state">State.</param>
        /// <param name="startDate">List notifications published after startDateTime</param>
        /// <param name="endDate">List notifications published before startDateTime</param>
        Task<int> GetUserNotificationCountAsync(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null);


        /// <summary>
        /// Updates a user notification state.
        /// </summary>
        /// <param name="userId">Tenant Id.</param>
        /// <param name="notificationId">The user notification id.</param>
        /// <param name="state">New state.</param>
        Task UpdateUserNotificationStateAsync(Guid userId, Guid notificationId, UserNotificationState state);

        /// <summary>
        /// Updates all notification states for a user.
        /// </summary>
        /// <param name="userId">User.</param>
        /// <param name="state">New state.</param>
        Task UpdateAllUserNotificationStatesAsync(Guid userId, UserNotificationState state);

        /// <summary>
        /// Deletes a user notification.
        /// </summary>
        /// <param name="userId">Tenant Id.</param>
        /// <param name="notificationId">The user notification id.</param>
        Task DeleteUserNotificationAsync(Guid userId, Guid notificationId);

        /// <summary>
        /// Deletes all notifications of a user.
        /// </summary>
        /// <param name="userId">User.</param>
        /// <param name="state">State</param>
        /// <param name="startDate">Delete notifications published after startDateTime</param>
        /// <param name="endDate">Delete notifications published before startDateTime</param>
        Task DeleteAllUserNotificationsAsync(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null);

    }
}