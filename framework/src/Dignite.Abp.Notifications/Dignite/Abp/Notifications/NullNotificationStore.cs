using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Notifications
{
    public class NullNotificationStore : INotificationStore, ITransientDependency
    {
        public Task DeleteAllUserNotificationsAsync(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            return Task.CompletedTask;
        }

        public Task DeleteSubscriptionAsync(Guid user, string notificationName, [CanBeNull] string entityTypeName, [CanBeNull] string entityId)
        {
            return Task.CompletedTask;
        }

        public Task DeleteUserNotificationAsync(Guid userId, Guid notificationId)
        {
            return Task.CompletedTask;
        }

        public async Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(string notificationName, [CanBeNull] string entityTypeName, [CanBeNull] string entityId)
        {
            List<NotificationSubscriptionInfo> subscriptions = null;
            return await Task.FromResult(subscriptions);
        }

        public async Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(Guid userId)
        {
            List<NotificationSubscriptionInfo> subscriptions = null;
            return await Task.FromResult(subscriptions);
        }

        public Task<int> GetUserNotificationCountAsync(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            return Task.FromResult(0);
        }

        public async Task<List<UserNotificationWithNotification>> GetUserNotificationsAsync(Guid userId, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue, DateTime? startDate = null, DateTime? endDate = null)
        {
            List<UserNotificationWithNotification> notifications = null;
            return await Task.FromResult(notifications);
        }

        public async Task<string[]> GetUserRoles(Guid userId)
        {
            string[] roles = null;
            return await Task.FromResult(roles);
        }

        public Task InsertNotificationAsync(NotificationInfo notification)
        {
            return Task.CompletedTask;
        }

        public Task InsertSubscriptionAsync(NotificationSubscriptionInfo subscription)
        {
            return Task.CompletedTask;
        }

        public Task InsertUserNotificationAsync(UserNotificationInfo userNotification)
        {
            return Task.CompletedTask;
        }

        public Task<bool> IsSubscribedAsync(Guid userId, string notificationName, [CanBeNull] string entityTypeName, [CanBeNull] string entityId)
        {
            return Task.FromResult(false);
        }

        public Task UpdateAllUserNotificationStatesAsync(Guid userId, UserNotificationState state)
        {
            return Task.CompletedTask;
        }

        public Task UpdateUserNotificationStateAsync(Guid userId, Guid notificationId, UserNotificationState state)
        {
            return Task.CompletedTask;
        }
    }
}
