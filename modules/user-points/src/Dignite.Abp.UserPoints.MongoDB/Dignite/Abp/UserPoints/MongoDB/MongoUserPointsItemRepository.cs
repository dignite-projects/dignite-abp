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

public class MongoUserPointsItemRepository : MongoDbRepository<IUserPointsMongoDbContext, UserPointsItem, Guid>, IUserPointsItemRepository
{
    public MongoUserPointsItemRepository(IMongoDbContextProvider<IUserPointsMongoDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public virtual async Task<int> GetCountAsync(Guid userId, string pointsDefinitionName = null, string pointsWorkflowName = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        return await (await GetQueryableAsync(cancellationToken)).Where(e => e.UserId == userId)
            .WhereIf(pointsDefinitionName.IsNullOrEmpty() && pointsWorkflowName.IsNullOrEmpty(), upi => upi.PointsType == PointsType.General)
            .WhereIf(!pointsDefinitionName.IsNullOrEmpty(), e => e.PointsDefinitionName == pointsDefinitionName)
            .WhereIf(!pointsWorkflowName.IsNullOrEmpty(), e => e.PointsWorkflowName == pointsWorkflowName)
            .WhereIf(startTime.HasValue, e => e.CreationTime >= startTime.Value)
            .WhereIf(endTime.HasValue, e => e.CreationTime < endTime.Value)
            .CountAsync(cancellationToken);
    }

    public virtual async Task<List<UserPointsItem>> GetListAsync(Guid userId, string pointsDefinitionName = null, string pointsWorkflowName = null, DateTime? startTime = null, DateTime? endTime = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        return await (await GetQueryableAsync(cancellationToken)).Where(e => e.UserId == userId)
            .WhereIf(pointsDefinitionName.IsNullOrEmpty() && pointsWorkflowName.IsNullOrEmpty(), upi => upi.PointsType == PointsType.General)
            .WhereIf(!pointsDefinitionName.IsNullOrEmpty(), e => e.PointsDefinitionName == pointsDefinitionName)
            .WhereIf(!pointsWorkflowName.IsNullOrEmpty(), e => e.PointsWorkflowName == pointsWorkflowName)
            .WhereIf(startTime.HasValue, e => e.CreationTime >= startTime.Value)
            .WhereIf(endTime.HasValue, e => e.CreationTime < endTime.Value)
            .OrderByDescending(un => un.CreationTime)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(cancellationToken);
    }
}
