using Dignite.Abp.NotificationCenter.EntityFrameworkCore;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Abp.NotificationCenter
{
    public class EfCoreNotificationSubscriptionRepository : EfCoreRepository<INotificationCenterDbContext, NotificationSubscription>, INotificationSubscriptionRepository
    {
        public EfCoreNotificationSubscriptionRepository(
            IDbContextProvider<INotificationCenterDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<NotificationSubscription> FindAsync(Guid userId, string notificationName, [CanBeNull] string entityTypeName, [CanBeNull] string entityId, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(entityTypeName!=null,un=>un.EntityTypeName==entityTypeName && un.EntityId==entityId)
                .FirstOrDefaultAsync(un =>
                    un.UserId == userId && un.NotificationName == notificationName,
                    GetCancellationToken(cancellationToken)
                );
        }

        public async Task<List<NotificationSubscription>> GetListAsync(string notificationName, [CanBeNull] string entityTypeName, [CanBeNull] string entityId, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(entityTypeName != null, un => un.EntityTypeName == entityTypeName && un.EntityId == entityId)
                .Where(un=>un.NotificationName==notificationName)
                .ToListAsync(
                    GetCancellationToken(cancellationToken)
                );
        }

        public async Task<List<NotificationSubscription>> GetListAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(un => un.UserId == userId)
                .ToListAsync(
                    GetCancellationToken(cancellationToken)
                );
        }
    }
}
