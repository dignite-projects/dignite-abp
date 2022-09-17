using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Dignite.Abp.NotificationCenter.MongoDB;
using Dignite.Abp.Notifications;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.NotificationCenter;

public class MongoUserNotificationRepository : MongoDbRepository<INotificationCenterMongoDbContext, UserNotification>, IUserNotificationRepository
{
    public MongoUserNotificationRepository(
        IMongoDbContextProvider<INotificationCenterMongoDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<UserNotification> FindAsync(Guid userId, Guid notificationId, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        return await (await GetMongoQueryableAsync(cancellationToken))
                   .FirstOrDefaultAsync(un =>
                       un.UserId == userId && un.NotificationId == notificationId,
                       cancellationToken
                   );
    }

    public async Task<List<UserNotification>> GetListAsync(Guid userId, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        return await (await GetMongoQueryableAsync(cancellationToken))
            .WhereIf(state != null, un => un.State == state)
            .WhereIf(startDate != null, un => un.CreationTime >= startDate.Value)
            .WhereIf(endDate != null, un => un.CreationTime <= endDate.Value)
            .As<IMongoQueryable<UserNotification>>()
            .Where(un =>
                un.UserId == userId
            )
            .OrderByDescending(un => un.CreationTime)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetCountAsync(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        return await (await GetMongoQueryableAsync(cancellationToken))
            .WhereIf(state != null, un => un.State == state)
            .WhereIf(startDate != null, un => un.CreationTime >= startDate.Value)
            .WhereIf(endDate != null, un => un.CreationTime <= endDate.Value)
            .As<IMongoQueryable<UserNotification>>()
            .Where(un =>
                un.UserId == userId
            )
            .CountAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync(Guid notificationId, Guid ignoredUserId, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        return await (await GetMongoQueryableAsync(cancellationToken))
                   .AnyAsync(un => un.NotificationId == notificationId && un.UserId != ignoredUserId, cancellationToken);
    }
}