using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Timing;

namespace Dignite.Abp.UserPoints.EntityFrameworkCore;
public class EfCoreUserPointsBlockRepository : EfCoreRepository<IUserPointsDbContext, UserPointsBlock, Guid>, IUserPointsBlockRepository
{
    private readonly IClock _clock;

    public EfCoreUserPointsBlockRepository(IDbContextProvider<IUserPointsDbContext> dbContextProvider,IClock clock)
        : base(dbContextProvider)
    {
        _clock = clock;
    }


    public async Task<List<UserPointsBlock>> GetTopAvailableListAsync(int top, Guid userId, PointsType pointsType = PointsType.General, string pointsDefinitionName = null, string pointsWorkflowName = null, CancellationToken cancellationToken = default)
    {
        var sdfasdf= await (await WithDetailsAsync()).ToListAsync();

        return await(await GetQueryableAsync(userId, pointsType, pointsDefinitionName, pointsWorkflowName))
            .Include(b=>b.UserPointsItem)
            .OrderBy(b=>b.UserPointsItem.ExpirationDate)
            .Take(top)
            .ToListAsync();
    }

    public async Task<int> GetUserAvailablePointsAsync(Guid userId, PointsType pointsType = PointsType.General, string pointsDefinitionName = null, string pointsWorkflowName = null, CancellationToken cancellationToken = default)
    {
        return await (await GetQueryableAsync(userId, pointsType, pointsDefinitionName, pointsWorkflowName))
            .SumAsync(b=>b.UserPointsItem.Points);
    }

    public override async Task<IQueryable<UserPointsBlock>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }

    protected virtual async Task<IQueryable<UserPointsBlock>> GetQueryableAsync(
         Guid userId, PointsType pointsType = PointsType.General, string pointsDefinitionName = null, string pointsWorkflowName = null)
    {
        return (await GetDbSetAsync()).Where(e => e.UserPointsItem.UserId == userId && e.UserPointsItem.PointsType == pointsType && !e.IsLocked && e.UserPointsItem.ExpirationDate> _clock.Now)
            .WhereIf(!pointsDefinitionName.IsNullOrEmpty(), e => e.UserPointsItem.PointsDefinitionName == pointsDefinitionName)
            .WhereIf(!pointsWorkflowName.IsNullOrEmpty(), e => e.UserPointsItem.PointsWorkflowName == pointsWorkflowName);
    }
}
