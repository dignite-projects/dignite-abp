using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Abp.UserPoints.EntityFrameworkCore;
public class EfCoreUserPointsOrderRepository : EfCoreRepository<IUserPointsDbContext, UserPointsOrder, Guid>, IUserPointsOrderRepository
{
    public EfCoreUserPointsOrderRepository(IDbContextProvider<IUserPointsDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }


    public async Task<int> GetCountAsync(Guid userId, DateTime? startTime = null, DateTime? endTime = null, CancellationToken cancellationToken = default)
    {
        return await(await GetQueryableAsync(userId, startTime, endTime))
            .CountAsync();
    }

    public async Task<List<UserPointsOrder>> GetListAsync(Guid userId, DateTime? startTime = null, DateTime? endTime = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        return await (await GetQueryableAsync(userId, startTime, endTime))
            .OrderByDescending(o=>o.CreationTime)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync();
    }

    protected virtual async Task<IQueryable<UserPointsOrder>> GetQueryableAsync(
         Guid? userId, DateTime? startTime = null, DateTime? endTime = null)
    {
        return (await GetDbSetAsync())
            .WhereIf(userId.HasValue, e => e.UserId == userId.Value)
            .WhereIf(startTime.HasValue, e => e.CreationTime >= startTime.Value)
            .WhereIf(endTime.HasValue, e => e.CreationTime < endTime.Value);
    }
}
