using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Threading;
using System.Threading.Tasks;
using Dignite.Abp.NotificationCenter.MongoDB;
using JetBrains.Annotations;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.NotificationCenter;

public class MongoNotificationSubscriptionRepository : MongoDbRepository<NotificationCenterMongoDbContext, NotificationSubscription>, INotificationSubscriptionRepository
{
    public MongoNotificationSubscriptionRepository(
        IMongoDbContextProvider<NotificationCenterMongoDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<NotificationSubscription> FindAsync(Guid userId, string notificationName, [CanBeNull] string entityTypeName, [CanBeNull] string entityId, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        return await (await GetMongoQueryableAsync(cancellationToken))
            .WhereIf(entityTypeName != null, un => un.EntityTypeName == entityTypeName && un.EntityId == entityId)
            .As<IMongoQueryable<NotificationSubscription>>()
            .FirstOrDefaultAsync(un =>
                un.UserId == userId && un.NotificationName == notificationName,
                GetCancellationToken(cancellationToken)
            );
    }

    public async Task<List<NotificationSubscription>> GetListAsync(string notificationName, [CanBeNull] string entityTypeName, [CanBeNull] string entityId, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        return await (await GetMongoQueryableAsync(cancellationToken))
            .WhereIf(entityTypeName != null, un => un.EntityTypeName == entityTypeName && un.EntityId == entityId)
            .As<IMongoQueryable<NotificationSubscription>>()
            .Where(un => un.NotificationName == notificationName)
            .ToListAsync(
                GetCancellationToken(cancellationToken)
            );
    }

    public async Task<List<NotificationSubscription>> GetListAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        return await (await GetMongoQueryableAsync(cancellationToken))
            .Where(un => un.UserId == userId)
            .ToListAsync(
                GetCancellationToken(cancellationToken)
            );
    }
}