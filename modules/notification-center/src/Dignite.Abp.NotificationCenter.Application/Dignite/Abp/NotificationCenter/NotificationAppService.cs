﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dignite.Abp.Notifications;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.NotificationCenter;

public class NotificationAppService : NotificationCenterAppService, INotificationAppService
{
    private readonly INotificationDefinitionManager _notificationDefinitionManager;
    private readonly INotificationSubscriptionManager _subscriptionManager;
    private readonly IUserNotificationManager _userNotificationManager;

    public NotificationAppService(
        INotificationDefinitionManager notificationDefinitionManager,
        INotificationSubscriptionManager subscriptionManager,
        IUserNotificationManager userNotificationManager
        )
    {
        _notificationDefinitionManager = notificationDefinitionManager;
        _subscriptionManager = subscriptionManager;
        _userNotificationManager = userNotificationManager;
    }

    /// <summary>
    /// Get the notification subscription of the current user
    /// </summary>
    /// <remarks>
    /// Contains notifications available and subscribed to by the current user
    /// </remarks>
    /// <returns></returns>
    [Authorize]
    public async Task<ListResultDto<NotificationSubscriptionDto>> GetAllAvailableSubscribeAsync()
    {
        var userId = CurrentUser.Id.Value;
        var subscribedNotifications = await _subscriptionManager.GetSubscribedNotificationsAsync(userId);
        var availableNotificationDefinitions = await _notificationDefinitionManager.GetAllAvailableAsync(userId);

        var dto = availableNotificationDefinitions.Select(nd=>
                new NotificationSubscriptionDto(
                    nd.Name,
                    nd.DisplayName?.Localize(StringLocalizerFactory),
                    nd.Description?.Localize(StringLocalizerFactory),
                    subscribedNotifications.Any(sn=>sn.NotificationName==nd.Name)
                )
            ).ToList();


        return new ListResultDto<NotificationSubscriptionDto>(dto);
    }

    /// <summary>
    /// Subscribes to a notification for current user and notification informations.
    /// </summary>
    /// <param name="notificationName">Name of the notification.</param>
    [Authorize]
    public async Task SubscribeAsync([NotNull] string notificationName)
    {
        await _subscriptionManager.SubscribeAsync(CurrentUser.Id.Value, notificationName);
    }

    /// <summary>
    /// Unsubscribe from current user's notification
    /// </summary>
    /// <returns></returns>
    [Authorize]
    public async Task UnsubscribeAsync([NotNull] string notificationName)
    {
        await _subscriptionManager.UnsubscribeAsync(CurrentUser.Id.Value, notificationName);
    }

    /// <summary>
    /// Updates user notification state.
    /// </summary>
    [Authorize]
    public async Task UpdateStateAsync([NotNull] Guid notificationId, [NotNull] UserNotificationState state)
    {
        await _userNotificationManager.UpdateUserNotificationStateAsync(CurrentUser.Id.Value, notificationId, state);
    }

    /// <summary>
    /// Updates all notification states for current user.
    /// </summary>
    [Authorize]
    public async Task UpdateAllStatesAsync([NotNull] UserNotificationState state)
    {
        await _userNotificationManager.UpdateAllUserNotificationStatesAsync(CurrentUser.Id.Value, state);
    }

    /// <summary>
    /// Deletes user notification.
    /// </summary>
    [Authorize]
    public async Task DeleteAsync([NotNull] Guid id)
    {
        await _userNotificationManager.DeleteUserNotificationAsync(CurrentUser.Id.Value, id);
    }

    /// <summary>
    /// Deletes all notifications of the current user.
    /// </summary>
    [Authorize]
    public async Task DeleteAllAsync(UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null)
    {
        await _userNotificationManager.DeleteAllUserNotificationsAsync(CurrentUser.Id.Value, state, startDate, endDate);
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
    public async Task<ListResultDto<UserNotificationDto>> GetListAsync(UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue, DateTime? startDate = null, DateTime? endDate = null)
    {
        var result = await _userNotificationManager.GetUserNotificationsAsync(CurrentUser.Id.Value, state, skipCount, maxResultCount, startDate, endDate);
        return new ListResultDto<UserNotificationDto>(
            result.Select(un => new UserNotificationDto(
                un.UserNotification.UserId,
                un.UserNotification.NotificationId,
                un.Notification.NotificationName,
                _notificationDefinitionManager.GetOrNull(un.Notification.NotificationName)?.DisplayName?.Localize(StringLocalizerFactory),
                un.Notification.Data,
                un.Notification.EntityTypeName,
                un.Notification.EntityId,
                un.Notification.Severity,
                un.Notification.CreationTime,
                un.UserNotification.State
            )).ToList()
        );
    }

    /// <summary>
    /// Gets user notification count.
    /// </summary>
    /// <param name="state">The state.</param>
    /// <param name="startDate">List notifications published after startDateTime</param>
    /// <param name="endDate">List notifications published before startDateTime</param>
    [Authorize]
    public async Task<int> GetCountAsync(UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null)
    {
        return await _userNotificationManager.GetUserNotificationCountAsync(CurrentUser.Id.Value, state, startDate, endDate);
    }
}