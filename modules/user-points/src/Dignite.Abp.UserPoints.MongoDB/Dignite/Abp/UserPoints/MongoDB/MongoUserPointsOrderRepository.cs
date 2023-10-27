using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.UserPoints.MongoDB;

public class MongoUserPointsOrderRepository : MongoDbRepository<IUserPointsMongoDbContext, UserPointsOrder, Guid>, IUserPointsOrderRepository
{
    public MongoUserPointsOrderRepository(IMongoDbContextProvider<IUserPointsMongoDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<UserPointsOrder> FindByBusinessOrderAsync(string businessOrderType, string businessOrderNumber, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        return await (await GetMongoQueryableAsync(cancellationToken))
            .FirstOrDefaultAsync(upo => upo.BusinessOrderType == businessOrderType && upo.BusinessOrderNumber == businessOrderNumber, cancellationToken);
    }

    public virtual async Task<int> GetCountAsync(Guid userId, DateTime? startTime = null, DateTime? endTime = null, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        return await(await GetMongoQueryableAsync(cancellationToken))
            .Where(e => e.UserId == userId)
            .WhereIf(startTime.HasValue, e => e.CreationTime >= startTime.Value)
            .WhereIf(endTime.HasValue, e => e.CreationTime < endTime.Value)
            .As<IMongoQueryable<UserPointsOrder>>()
            .CountAsync(cancellationToken);
    }

    public virtual async Task<List<UserPointsOrder>> GetListAsync(Guid userId, DateTime? startTime = null, DateTime? endTime = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        return await (await GetMongoQueryableAsync(cancellationToken))
            .Where(e => e.UserId == userId)
            .WhereIf(startTime.HasValue, e => e.CreationTime >= startTime.Value)
            .WhereIf(endTime.HasValue, e => e.CreationTime < endTime.Value)
            .As<IMongoQueryable<UserPointsOrder>>()
            .OrderByDescending(un => un.CreationTime)
            .PageBy<UserPointsOrder, IMongoQueryable<UserPointsOrder>>(skipCount, maxResultCount)
            .ToListAsync(cancellationToken);
    }
}
