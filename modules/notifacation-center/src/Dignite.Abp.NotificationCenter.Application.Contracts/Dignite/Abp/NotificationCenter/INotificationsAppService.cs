using Dignite.Abp.Notifications;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.NotificationCenter
{
    public interface INotificationsAppService : IApplicationService
    {
        /// <summary>
        /// Get the notification subscription of the current user
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<NotificationSubscriptionDto>> GetSubscribedAsync();

        /// <summary>
        /// Unsubscribe from current user's notification
        /// </summary>
        /// <returns></returns>
        Task UnsubscribeAsync([NotNull] string notificationName);

        /// <summary>
        /// Updates user notification state.
        /// </summary>
        Task UpdateStateAsync([NotNull] Guid notificationId, [NotNull] UserNotificationState state);

        /// <summary>
        /// Updates all notification states for current user.
        /// </summary>
        Task UpdateStatesAsync([NotNull] UserNotificationState state);

        /// <summary>
        /// Deletes user notification.
        /// </summary>
        Task DeleteAsync([NotNull] Guid notificationId);

        /// <summary>
        /// Deletes all notifications of the current user.
        /// </summary>
        Task DeleteAllAsync(UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Gets notifications of the current user.
        /// </summary>
        /// <param name="state">State</param>
        /// <param name="skipCount">Skip count.</param>
        /// <param name="maxResultCount">Maximum result count.</param>
        /// <param name="startDate">List notifications published after startDateTime</param>
        /// <param name="endDate">List notifications published before startDateTime</param>
        Task<ListResultDto<UserNotificationDto>> GetListAsync( UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue, DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Gets user notification count.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="startDate">List notifications published after startDateTime</param>
        /// <param name="endDate">List notifications published before startDateTime</param>
        Task<int> GetCountAsync( UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null);

    }
}
