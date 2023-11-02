using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using Volo.Abp.Timing;

namespace Dignite.Abp.UserPoints.MongoDB;

public class MongoUserPointsBlockRepository : MongoDbRepository<IUserPointsMongoDbContext, UserPointsBlock, Guid>, IUserPointsBlockRepository
{
    private readonly IClock _clock;
    public MongoUserPointsBlockRepository(IMongoDbContextProvider<IUserPointsMongoDbContext> dbContextProvider, IClock clock)
        : base(dbContextProvider)
    {
        _clock = clock;
    }


    public virtual async Task<List<UserPointsBlock>> GetTopAvailableListAsync(int top, Guid userId, PointsType pointsType = PointsType.General, string pointsDefinitionName = null, string pointsWorkflowName = null,
        CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        return await(await GetMongoQueryableAsync(cancellationToken))
            .Where(e => !e.UserPointsItem.IsDeleted && e.UserPointsItem.UserId == userId && e.UserPointsItem.PointsType == pointsType && !e.IsLocked && e.UserPointsItem.ExpirationDate > _clock.Now)
            .WhereIf(!pointsDefinitionName.IsNullOrEmpty(), e => e.UserPointsItem.PointsDefinitionName == pointsDefinitionName)
            .WhereIf(!pointsWorkflowName.IsNullOrEmpty(), e => e.UserPointsItem.PointsWorkflowName == pointsWorkflowName)
            .As<IMongoQueryable<UserPointsBlock>>()
            .OrderBy(un => un.UserPointsItem.ExpirationDate)
            .Take(top)
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<int> GetUserAvailablePointsAsync(Guid userId, DateTime? expirationDate = null, PointsType pointsType = PointsType.General, string pointsDefinitionName = null, string pointsWorkflowName = null,
        CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        return await(await GetMongoQueryableAsync(cancellationToken))
            .Where(e => !e.UserPointsItem.IsDeleted && e.UserPointsItem.UserId == userId && e.UserPointsItem.PointsType == pointsType && !e.IsLocked && e.UserPointsItem.ExpirationDate > _clock.Now)
            .WhereIf(expirationDate.HasValue, e => e.UserPointsItem.ExpirationDate < expirationDate)
            .WhereIf(!pointsDefinitionName.IsNullOrEmpty(), e => e.UserPointsItem.PointsDefinitionName == pointsDefinitionName)
            .WhereIf(!pointsWorkflowName.IsNullOrEmpty(), e => e.UserPointsItem.PointsWorkflowName == pointsWorkflowName)
            .As<IMongoQueryable<UserPointsBlock>>()
            .CountAsync(cancellationToken);
    }
}
