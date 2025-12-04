using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Volo.Abp.Timing;

namespace Dignite.Abp.UserPoints.EntityFrameworkCore;
public class EfCoreUserPointRepository : EfCoreRepository<IUserPointsDbContext, UserPoint, Guid>, IUserPointRepository
{
    private readonly IClock _clock;
    public EfCoreUserPointRepository(IDbContextProvider<IUserPointsDbContext> dbContextProvider, IClock clock)
        : base(dbContextProvider)
    {
        _clock = clock;
    }

    public async Task<UserPoint> CalibrateBalanceAsync(
        Guid userId,
        CancellationToken cancellationToken = default
         )
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        var userPoint = await (await GetDbSetAsync())
            .Where(up => up.UserId == userId)
            .OrderByDescending(up => up.CreationTime)
            .FirstOrDefaultAsync(cancellationToken);
        if (userPoint != null && userPoint.NextExpirationAt.HasValue && userPoint.NextExpirationAt.Value < _clock.Now)
        {
            var expiredUserPoints = await (await GetDbSetAsync())
                .Where(up => up.UserId == userId && up.ExpirationTime.HasValue && up.ExpirationTime <= _clock.Now && up.ExpirationTime >= userPoint.NextExpirationAt.Value)
                .ToListAsync(cancellationToken);
            var nextExpiringUserPoint = await (await GetDbSetAsync())
                .Where(up => up.UserId == userId && up.ExpirationTime.HasValue && up.ExpirationTime > _clock.Now)
                .OrderBy(up => up.ExpirationTime)
                .FirstOrDefaultAsync(cancellationToken);
            userPoint.SetBalance(
                userPoint.Balance - expiredUserPoints.Sum(ep => ep.Amount),
                nextExpiringUserPoint?.ExpirationTime
                );
            await UpdateAsync(userPoint, cancellationToken: cancellationToken);
        }

        return userPoint;
    }

    public async Task<int> GetCountAsync(
        Guid userId,
        string pointType = null,
        int? minAmount = null,
        int? maxAmount = null,
        DateTime? startExpirationTime = null,
        DateTime? endExpirationTime = null,
        string entityType = null,
        string entityId = null,
        CancellationToken cancellationToken = default)
    {
        return await(await GetQueryableAsync(userId, pointType, minAmount, maxAmount, startExpirationTime, endExpirationTime, entityType, entityId))
            .CountAsync(GetCancellationToken(cancellationToken));
    }

    public async Task<List<UserPoint>> GetListAsync(
        Guid userId,
        string pointType = null,
        int? minAmount = null,
        int? maxAmount = null,
        DateTime? startExpirationTime = null,
        DateTime? endExpirationTime = null,
        string entityType = null,
        string entityId = null, 
        int skipCount = 0, 
        int maxResultCount = int.MaxValue, 
        string sorting=null,
        CancellationToken cancellationToken = default)
    {
        var queryable = await GetQueryableAsync(userId, pointType, minAmount, maxAmount, startExpirationTime, endExpirationTime, entityType, entityId);
        queryable = queryable.OrderBy(sorting.IsNullOrEmpty() ? $"{nameof(UserPoint.CreationTime)} desc" : sorting);
        return await queryable
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public override async Task<IQueryable<UserPoint>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }

    protected virtual async Task<IQueryable<UserPoint>> GetQueryableAsync(
        Guid userId,
        string pointType = null,
        int? minAmount = null,
        int? maxAmount = null,
        DateTime? startExpirationTime = null,
        DateTime? endExpirationTime = null,
        string entityType = null,
        string entityId = null)
    {
        return (await GetDbSetAsync()).Where(up => up.UserId == userId)
            .WhereIf(!pointType.IsNullOrEmpty(), up => up.PointType == pointType)
            .WhereIf(minAmount.HasValue, up => up.Amount >= minAmount.Value)
            .WhereIf(maxAmount.HasValue, up => up.Amount < maxAmount.Value)
            .WhereIf(startExpirationTime.HasValue, up => up.ExpirationTime.HasValue && up.ExpirationTime >= startExpirationTime.Value)
            .WhereIf(endExpirationTime.HasValue, up => up.ExpirationTime.HasValue && up.ExpirationTime < endExpirationTime.Value)
            .WhereIf(!entityType.IsNullOrEmpty(), up => up.EntityType == entityType)
            .WhereIf(!entityId.IsNullOrEmpty(), up => up.EntityId == entityId);
    }
}
