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

    public async Task<List<string>> GetEntityIdsListByUserAsync([NotNull] string entityType, Guid userId, DateTime? visitDate = null, CancellationToken cancellationToken = default)
    {
        return await(await GetMongoQueryableAsync(cancellationToken))
            .Where(r => r.EntityType == entityType && r.CreatorId == userId)
            .WhereIf(visitDate.HasValue, v => v.CreationTime > visitDate.Value.Date && v.CreationTime < visitDate.Value.Date.AddDays(1))
            .GroupBy(v => v.EntityId)
            .Select(v => v.Key)
            .As<IMongoQueryable<string>>()
            .ToListAsync(GetCancellationToken(cancellationToken));
    }
}
