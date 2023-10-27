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
using Volo.Abp.Timing;

namespace Dignite.Abp.UserPoints.MongoDB;

public class MongoUserPointsItemRepository : MongoDbRepository<IUserPointsMongoDbContext, UserPointsItem, Guid>, IUserPointsItemRepository
{
    private readonly IClock _clock;
    public MongoUserPointsItemRepository(IMongoDbContextProvider<IUserPointsMongoDbContext> dbContextProvider, IClock clock)
        : base(dbContextProvider)
    {
        _clock = clock;
    }

    public virtual async Task<int> GetUserTotalPointsAsync(Guid userId, DateTime? expirationDate, PointsType pointsType = PointsType.General, string pointsDefinitionName = null, string pointsWorkflowName = null, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        return await (await GetMongoQueryableAsync(cancellationToken)).Where(e => e.UserId == userId && e.PointsType == pointsType)
            .WhereIf(expirationDate.HasValue, e => e.ExpirationDate < expirationDate && e.ExpirationDate > _clock.Now)
            .WhereIf(!pointsDefinitionName.IsNullOrEmpty(), e => e.PointsDefinitionName == pointsDefinitionName)
            .WhereIf(!pointsWorkflowName.IsNullOrEmpty(), e => e.PointsWorkflowName == pointsWorkflowName)
            .As<IMongoQueryable<UserPointsItem>>()
            .SumAsync(p=>p.Points,cancellationToken);
    }

    public virtual async Task<int> GetCountAsync(Guid userId, PointsType pointsType = PointsType.General, string pointsDefinitionName = null, string pointsWorkflowName = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        return await (await GetMongoQueryableAsync(cancellationToken)).Where(e => e.UserId == userId && e.PointsType == pointsType)
            .WhereIf(!pointsDefinitionName.IsNullOrEmpty(), e => e.PointsDefinitionName == pointsDefinitionName)
            .WhereIf(!pointsWorkflowName.IsNullOrEmpty(), e => e.PointsWorkflowName == pointsWorkflowName)
            .WhereIf(startTime.HasValue, e => e.CreationTime >= startTime.Value)
            .WhereIf(endTime.HasValue, e => e.CreationTime < endTime.Value)
            .As<IMongoQueryable<UserPointsItem>>()
            .CountAsync(cancellationToken);
    }

    public virtual async Task<List<UserPointsItem>> GetListAsync(Guid userId, PointsType pointsType = PointsType.General, string pointsDefinitionName = null, string pointsWorkflowName = null, DateTime? startTime = null, DateTime? endTime = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        return await (await GetMongoQueryableAsync(cancellationToken)).Where(e => e.UserId == userId && e.PointsType == pointsType)
            .WhereIf(!pointsDefinitionName.IsNullOrEmpty(), e => e.PointsDefinitionName == pointsDefinitionName)
            .WhereIf(!pointsWorkflowName.IsNullOrEmpty(), e => e.PointsWorkflowName == pointsWorkflowName)
            .WhereIf(startTime.HasValue, e => e.CreationTime >= startTime.Value)
            .WhereIf(endTime.HasValue, e => e.CreationTime < endTime.Value)
            .As<IMongoQueryable<UserPointsItem>>()
            .OrderByDescending(un => un.CreationTime)
            .PageBy<UserPointsItem, IMongoQueryable<UserPointsItem>>(skipCount, maxResultCount)
            .ToListAsync(cancellationToken);
    }
}
