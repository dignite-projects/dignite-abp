using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;

namespace Dignite.Abp.Notifications
{
    /// <summary>
    /// Implements <see cref="INotificationSubscriptionManager"/>.
    /// </summary>
    public class NotificationSubscriptionManager : INotificationSubscriptionManager, ITransientDependency
    {
        private readonly INotificationStore _store;
        private readonly INotificationDefinitionManager _notificationDefinitionManager;
        private readonly IClock _clock;
        private readonly ICurrentTenant _currentTenant;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationSubscriptionManager"/> class.
        /// </summary>
        public NotificationSubscriptionManager(
            INotificationStore store, 
            INotificationDefinitionManager notificationDefinitionManager,
            IClock clock,
            ICurrentTenant currentTenant)
        {
            _store = store;
            _notificationDefinitionManager = notificationDefinitionManager;
            _clock = clock;
            _currentTenant = currentTenant;
        }

        public async Task SubscribeAsync(Guid userId, string notificationName, NotificationEntityIdentifier entityIdentifier = null)
        {
            if (await IsSubscribedAsync(userId, notificationName, entityIdentifier))
            {
                return;
            }

            await _store.InsertSubscriptionAsync(
                new NotificationSubscriptionInfo(
                    userId,
                    notificationName,
                    entityIdentifier == null ? null : entityIdentifier.Type.FullName,
                    entityIdentifier == null ? null : entityIdentifier.Id,
                    _clock.Now,
                    _currentTenant.Id
                    )
                );
        }

        public async Task SubscribeToAllAvailableNotificationsAsync(Guid userId)
        {            
            var notificationDefinitions = (
                await _notificationDefinitionManager.GetAllAvailableAsync(userId)
                )
                .Where(nd => nd.EntityType == null)
                .ToList();

            foreach (var notificationDefinition in notificationDefinitions)
            {
                await SubscribeAsync(userId, notificationDefinition.Name);
            }            
        }


        public async Task UnsubscribeAsync(Guid userId, string notificationName, NotificationEntityIdentifier entityIdentifier = null)
        {
            await _store.DeleteSubscriptionAsync(
                userId,
                notificationName,
                entityIdentifier == null ? null : entityIdentifier.Type.FullName,
                entityIdentifier == null ? null : entityIdentifier.Id
                );
        }

        public async Task<List<NotificationSubscriptionInfo>> GetSubscriptionsAsync(string notificationName, NotificationEntityIdentifier entityIdentifier = null)
        {
            var notificationSubscriptionInfos = await _store.GetSubscriptionsAsync(
                notificationName,
                entityIdentifier == null ? null : entityIdentifier.Type.FullName,
                entityIdentifier == null ? null : entityIdentifier.Id
                );

            return notificationSubscriptionInfos;
        }

        public async Task<List<NotificationSubscriptionInfo>> GetSubscribedNotificationsAsync(Guid userId)
        {
            var notificationSubscriptionInfos = await _store.GetSubscriptionsAsync(userId);

            return notificationSubscriptionInfos;
        }

        public Task<bool> IsSubscribedAsync(Guid userId, string notificationName, NotificationEntityIdentifier entityIdentifier = null)
        {
            return _store.IsSubscribedAsync(
                userId,
                notificationName,
                entityIdentifier == null ? null : entityIdentifier.Type.FullName,
                entityIdentifier == null ? null : entityIdentifier.Id
                );
        }
    }
}