using System;
using System.Threading.Tasks;
using Dignite.Abp.Notifications;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.NotificationCenter
{
    [RemoteService(Name = NotificationCenterRemoteServiceConsts.RemoteServiceName)]
    [Area("notifications")]
    [Route("api/notifications")]
    public class NotificationsController : NotificationCenterController, INotificationsAppService
    {
        private readonly INotificationsAppService _notificationsAppService;

        public NotificationsController(INotificationsAppService notificationAppService)
        {
            _notificationsAppService = notificationAppService;
        }

        /// <summary>
        /// Get the notification subscription of the current user
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("subscribed")]
        public async Task<ListResultDto<NotificationSubscriptionDto>> GetSubscribedAsync()
        {
            return await _notificationsAppService.GetSubscribedAsync();
        }

        /// <summary>
        /// Unsubscribe from current user's notification
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [Route("unsubscribe")]
        public async Task UnsubscribeAsync([NotNull] string notificationName)
        {
            await _notificationsAppService.UnsubscribeAsync(notificationName);
        }

        /// <summary>
        /// Updates user notification state.
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("update-state/{notificationId}")]
        public async Task UpdateStateAsync([NotNull] Guid notificationId, [NotNull] UserNotificationState state)
        {
            await _notificationsAppService.UpdateStateAsync(notificationId, state);
        }

        /// <summary>
        /// Updates all notification states for current user.
        /// </summary>
        [Authorize]
        [HttpPut]
        [Route("update-all-state")]
        public async Task UpdateStatesAsync([NotNull] UserNotificationState state)
        {
            await _notificationsAppService.UpdateStatesAsync( state);
        }

        /// <summary>
        /// Deletes user notification.
        /// </summary>
        [Authorize]
        [HttpDelete]
        [Route("{notificationId:Guid}")]
        public async Task DeleteAsync([NotNull] Guid notificationId)
        {
            await _notificationsAppService.DeleteAsync(notificationId);
        }

        /// <summary>
        /// Deletes all notifications of the current user.
        /// </summary>
        [Authorize]
        [HttpDelete]
        public async Task DeleteAllAsync(UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            await _notificationsAppService.DeleteAllAsync(state,startDate,endDate);
        }

        /// <summary>
        /// Gets notifications of the current user.
        /// </summary>
        /// <param name="state">State</param>
        /// <param name="skipCount">Skip count.</param>
        /// <param name="maxResultCount">Maximum result count.</param>
        /// <param name="startDate">List notifications published after startDateTime</param>
        /// <param name="endDate">List notifications published before startDateTime</param>
        [Authorize]
        [HttpGet]
        public async Task<ListResultDto<UserNotificationDto>> GetListAsync(UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue, DateTime? startDate = null, DateTime? endDate = null)
        {
            return await _notificationsAppService.GetListAsync(state, skipCount, maxResultCount, startDate, endDate);
        }

        /// <summary>
        /// Gets user notification count.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="startDate">List notifications published after startDateTime</param>
        /// <param name="endDate">List notifications published before startDateTime</param>
        [Authorize]
        [HttpGet]
        [Route("count")]
        public async Task<int> GetCountAsync(UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            return await _notificationsAppService.GetCountAsync(state, startDate, endDate);
        }

    }
}
