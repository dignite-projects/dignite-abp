using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dignite.Abp.NotificationCenter.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.NotificationCenter;

public class MongoNotificationRepository : MongoDbRepository<INotificationCenterMongoDbContext, Notification, Guid>, INotificationRepository
{
    public MongoNotificationRepository(
        IMongoDbContextProvider<INotificationCenterMongoDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<List<Notification>> GetListAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        return await (await GetMongoQueryableAsync(cancellationToken))
            .Where(n =>
                ids.Contains(n.Id)
            )
            .OrderByDescending(un => un.CreationTime)
            .ToListAsync(cancellationToken);
    }
}