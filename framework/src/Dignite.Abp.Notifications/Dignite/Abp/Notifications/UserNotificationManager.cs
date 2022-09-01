using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Notifications
{
    /// <summary>
    /// Implements  <see cref="IUserNotificationManager"/>.
    /// </summary>
    public class UserNotificationManager : IUserNotificationManager, ISingletonDependency
    {
        private readonly INotificationStore _store;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotificationManager"/> class.
        /// </summary>
        public UserNotificationManager(INotificationStore store)
        {
            _store = store;
        }

        public async Task<List<UserNotificationWithNotification>> GetUserNotificationsAsync(Guid userId, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue, DateTime? startDate = null, DateTime? endDate = null)            
        {
            return await _store.GetUserNotificationsAsync(userId, state, skipCount, maxResultCount, startDate, endDate);            
        }

        public Task<int> GetUserNotificationCountAsync(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            return _store.GetUserNotificationCountAsync(userId, state, startDate, endDate);
        }


        public Task UpdateUserNotificationStateAsync(Guid userId, Guid notificationId, UserNotificationState state)
        {
            return _store.UpdateUserNotificationStateAsync(userId, notificationId, state);
        }


        public Task UpdateAllUserNotificationStatesAsync(Guid userId, UserNotificationState state)
        {
            return _store.UpdateAllUserNotificationStatesAsync(userId, state);
        }

        public Task DeleteUserNotificationAsync(Guid userId, Guid notificationId)
        {
            return _store.DeleteUserNotificationAsync(userId, notificationId);
        }


        public Task DeleteAllUserNotificationsAsync(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            return _store.DeleteAllUserNotificationsAsync(userId, state, startDate, endDate);
        }

    }
}