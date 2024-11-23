using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dignite.CmsKit.Visits;
using JetBrains.Annotations;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace Dignite.CmsKit.MongoDB.Visits;

public class MongoVisitRepository : MongoDbRepository<ICmsKitMongoDbContext, Visit, Guid>, IVisitRepository
{
    public MongoVisitRepository(IMongoDbContextProvider<ICmsKitMongoDbContext> dbContextProvider) : base(
        dbContextProvider)
    {
    }

    public async Task<long> GetCountAsync(string entityType = null, string entityId = null, string osName = null, Guid? creatorId = null, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);

        return await (await GetMongoQueryableAsync(cancellationToken))
            .WhereIf<Visit, IMongoQueryable<Visit>>(!string.IsNullOrWhiteSpace(entityType), x => x.EntityType==entityType)
            .WhereIf<Visit, IMongoQueryable<Visit>>(!string.IsNullOrWhiteSpace(entityId), x => x.EntityId == entityId)
            .WhereIf<Visit, IMongoQueryable<Visit>>(!string.IsNullOrWhiteSpace(osName), x => x.DeviceInfo.StartsWith(entityId))
            .WhereIf<Visit, IMongoQueryable<Visit>>(creatorId.HasValue, x => x.CreatorId == creatorId)
            .CountAsync(cancellationToken);
    }

    public async Task<List<string>> GetEntityIdsFilteredByUserAsync(Guid userId, [NotNull] string entityType, int skipCount = 0, int maxResultCount = 100, CancellationToken cancellationToken = default)
    {
        return await(await GetMongoQueryableAsync(cancellationToken))
            .Where(r => r.EntityType == entityType && r.CreatorId == userId)
            .GroupBy(v => v.EntityId)
            .Select(v => v.Key)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public async Task<List<Visit>> GetListAsync(string entityType = null, string entityId = null, string osName = null, Guid? creatorId = null, int skipCount = 0, int maxResultCount = 100, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);

        return await (await GetMongoQueryableAsync(cancellationToken))
            .WhereIf<Visit, IMongoQueryable<Visit>>(!string.IsNullOrWhiteSpace(entityType), x => x.EntityType == entityType)
            .WhereIf<Visit, IMongoQueryable<Visit>>(!string.IsNullOrWhiteSpace(entityId), x => x.EntityId == entityId)
            .WhereIf<Visit, IMongoQueryable<Visit>>(!string.IsNullOrWhiteSpace(osName), x => x.DeviceInfo.StartsWith(entityId))
            .WhereIf<Visit, IMongoQueryable<Visit>>(creatorId.HasValue, x => x.CreatorId == creatorId)
            .OrderByDescending(x => x.CreationTime)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync(cancellationToken);
    }
}
