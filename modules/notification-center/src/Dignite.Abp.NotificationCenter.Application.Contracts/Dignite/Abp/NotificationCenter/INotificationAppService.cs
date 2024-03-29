﻿using System;
using System.Threading.Tasks;
using Dignite.Abp.Notifications;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.NotificationCenter;

public interface INotificationAppService : IApplicationService
{
    /// <summary>
    /// Get the notification subscription of the current user
    /// </summary>
    /// <remarks>
    /// Contains notifications available and subscribed to by the current user
    /// </remarks>
    /// <returns></returns>
    Task<ListResultDto<NotificationSubscriptionDto>> GetAllAvailableSubscribeAsync();


    /// <summary>
    /// Subscribes to a notification for current user and notification informations.
    /// </summary>
    /// <param name="notificationName">Name of the notification.</param>
    Task SubscribeAsync([NotNull] string notificationName);

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
    Task UpdateAllStatesAsync([NotNull] UserNotificationState state);

    /// <summary>
    /// Deletes user notification.
    /// </summary>
    Task DeleteAsync([NotNull] Guid id);

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
    Task<ListResultDto<UserNotificationDto>> GetListAsync(UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue, DateTime? startDate = null, DateTime? endDate = null);

    /// <summary>
    /// Gets user notification count.
    /// </summary>
    /// <param name="state">The state.</param>
    /// <param name="startDate">List notifications published after startDateTime</param>
    /// <param name="endDate">List notifications published before startDateTime</param>
    Task<int> GetCountAsync(UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null);
}