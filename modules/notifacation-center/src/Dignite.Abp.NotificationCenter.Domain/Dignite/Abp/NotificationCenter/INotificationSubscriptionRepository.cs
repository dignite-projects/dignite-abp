using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Abp.NotificationCenter
{
    public interface INotificationSubscriptionRepository : IBasicRepository<NotificationSubscription>
    {
        Task<NotificationSubscription> FindAsync(Guid userId, string notificationName, [CanBeNull]string entityTypeName, [CanBeNull] string entityId, CancellationToken cancellationToken = default);

        Task<List<NotificationSubscription>> GetListAsync(string notificationName, [CanBeNull] string entityTypeName, [CanBeNull] string entityId, CancellationToken cancellationToken = default);

        Task<List<NotificationSubscription>> GetListAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
