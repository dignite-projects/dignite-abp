using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Abp.UserPoints.EntityFrameworkCore;
public class EfCoreUserPointsItemRepository : EfCoreRepository<IUserPointsDbContext, UserPointsItem, Guid>, IUserPointsItemRepository
{
    public EfCoreUserPointsItemRepository(IDbContextProvider<IUserPointsDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }


    public async Task<int> GetCountAsync(Guid userId, string pointsDefinitionName = null, string pointsWorkflowName = null, DateTime? StartTime = null, DateTime? EndTime = null, CancellationToken cancellationToken = default)
    {
        return await(await GetQueryableAsync(userId,  pointsDefinitionName, pointsWorkflowName, StartTime, EndTime))
            .CountAsync();
    }

    public async Task<List<UserPointsItem>> GetListAsync(Guid userId,  string pointsDefinitionName = null, string pointsWorkflowName = null, DateTime? StartTime = null, DateTime? EndTime = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        return await(await GetQueryableAsync(userId, pointsDefinitionName, pointsWorkflowName, StartTime, EndTime))
            .OrderByDescending(o => o.CreationTime)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync();
    }

    public override async Task<IQueryable<UserPointsItem>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }

    protected virtual async Task<IQueryable<UserPointsItem>> GetQueryableAsync(
         Guid userId, string pointsDefinitionName = null, string pointsWorkflowName = null, DateTime? startTime = null, DateTime? endTime = null)
    {
        return (await GetDbSetAsync()).Where(upi => upi.UserId == userId)
            .WhereIf(pointsDefinitionName.IsNullOrEmpty() && pointsWorkflowName.IsNullOrEmpty(), upi => upi.PointsType== PointsType.General)
            .WhereIf(!pointsDefinitionName.IsNullOrEmpty(), upi => upi.PointsDefinitionName == pointsDefinitionName)
            .WhereIf(!pointsWorkflowName.IsNullOrEmpty(), upi => upi.PointsWorkflowName == pointsWorkflowName)
            .WhereIf(startTime.HasValue, upi => upi.CreationTime >= startTime.Value)
            .WhereIf(endTime.HasValue, upi => upi.CreationTime < endTime.Value);
    }
}
