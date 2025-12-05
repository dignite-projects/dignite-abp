using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using Volo.Abp.Timing;

namespace Dignite.Abp.UserPoints.MongoDB;

public class MongoUserPointRepository : MongoDbRepository<IUserPointsMongoDbContext, UserPoint, Guid>, IUserPointRepository
{
    private readonly IClock _clock;
    public MongoUserPointRepository(IMongoDbContextProvider<IUserPointsMongoDbContext> dbContextProvider, IClock clock)
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
        var userPoint = await (await GetMongoQueryableAsync(cancellationToken))
            .Where(up => up.UserId == userId)
            .OrderByDescending(up => up.CreationTime)
            .FirstOrDefaultAsync(cancellationToken);
        if (userPoint != null && userPoint.NextExpirationAt.HasValue && userPoint.NextExpirationAt.Value < _clock.Now)
        {
            var expiredUserPoints = await (await GetMongoQueryableAsync(cancellationToken))
                .Where(up => up.UserId == userId && up.ExpirationTime.HasValue && up.ExpirationTime <= _clock.Now && up.ExpirationTime >= userPoint.NextExpirationAt.Value)
                .ToListAsync(cancellationToken);
            var nextExpiringUserPoint = await (await GetMongoQueryableAsync(cancellationToken))
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

    public async Task ConsumeByExpirationAsync(
        Guid userId,
        [ValueRange(int.MinValue, -1)] int amount,
        CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);

        // amount 为负数，表示需要扣多少
        // 例如：amount = -20

        // 取得所有仍未过期的积分记录（按过期时间升序）
        var expiredUserPoints = await (await GetMongoQueryableAsync(cancellationToken))
            .Where(up => up.UserId == userId
                && up.ExpirationTime.HasValue
                && up.ExpirationTime >= _clock.Now)
            .OrderBy(up => up.ExpirationTime)
            .ToListAsync(cancellationToken);

        foreach (var userPoint in expiredUserPoints)
        {
            if (amount == 0)
                break;

            // remaining needed points as positive number
            var needed = -amount; // 例如：amount = -20 → needed = 20

            var consumableAmount = Math.Min(needed, userPoint.AvailableAmount.Value);
            // consumableAmount 的含义：在本条记录中最多能扣多少

            // 扣减可用积分
            userPoint.SetAvailableAmount(userPoint.AvailableAmount.Value - consumableAmount);

            // 更新剩余 amount（仍为负数）
            amount += consumableAmount;
            // 例如：amount = -20 + 6 → -14  

            await UpdateAsync(userPoint, cancellationToken: cancellationToken);
        }

        // 退出循环后，如果 amount < 0，表示本阶段已扣尽可扣分数        
    }

    public virtual async Task<int> GetCountAsync(
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
        cancellationToken = GetCancellationToken(cancellationToken);
        return await (await GetMongoQueryableAsync(cancellationToken)).Where(up => up.UserId == userId)
            .WhereIf(!pointType.IsNullOrEmpty(), up => up.PointType == pointType)
            .WhereIf(minAmount.HasValue, up => up.Amount >= minAmount.Value)
            .WhereIf(maxAmount.HasValue, up => up.Amount < maxAmount.Value)
            .WhereIf(startExpirationTime.HasValue, up => up.ExpirationTime.HasValue && up.ExpirationTime >= startExpirationTime.Value)
            .WhereIf(endExpirationTime.HasValue, up => up.ExpirationTime.HasValue && up.ExpirationTime < endExpirationTime.Value)
            .WhereIf(!entityType.IsNullOrEmpty(), up => up.EntityType == entityType)
            .WhereIf(!entityId.IsNullOrEmpty(), up => up.EntityId == entityId)
            .As<IMongoQueryable<UserPoint>>()
            .CountAsync(cancellationToken);
    }

    public virtual async Task<List<UserPoint>> GetListAsync(
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
        string sorting = null,
        CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        var queryable = (await GetMongoQueryableAsync(cancellationToken)).Where(up => up.UserId == userId)
            .WhereIf(!pointType.IsNullOrEmpty(), up => up.PointType == pointType)
            .WhereIf(minAmount.HasValue, up => up.Amount >= minAmount.Value)
            .WhereIf(maxAmount.HasValue, up => up.Amount < maxAmount.Value)
            .WhereIf(startExpirationTime.HasValue, up => up.ExpirationTime.HasValue && up.ExpirationTime >= startExpirationTime.Value)
            .WhereIf(endExpirationTime.HasValue, up => up.ExpirationTime.HasValue && up.ExpirationTime < endExpirationTime.Value)
            .WhereIf(!entityType.IsNullOrEmpty(), up => up.EntityType == entityType)
            .WhereIf(!entityId.IsNullOrEmpty(), up => up.EntityId == entityId);

        queryable = queryable.OrderBy(sorting.IsNullOrEmpty() ? $"{nameof(UserPoint.CreationTime)} desc" : sorting);

        return await queryable.PageBy(skipCount, maxResultCount)
            .As<IMongoQueryable<UserPoint>>()
            .ToListAsync(cancellationToken);
    }
}
