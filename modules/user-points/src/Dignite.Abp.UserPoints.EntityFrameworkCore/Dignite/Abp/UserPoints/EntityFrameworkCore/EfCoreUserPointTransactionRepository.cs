using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Timing;

namespace Dignite.Abp.UserPoints.EntityFrameworkCore;
public class EfCoreUserPointTransactionRepository : EfCoreRepository<IUserPointsDbContext, UserPointTransaction, Guid>, IUserPointTransactionRepository
{
    private readonly IClock _clock;
    public EfCoreUserPointTransactionRepository(IDbContextProvider<IUserPointsDbContext> dbContextProvider, IClock clock)
        : base(dbContextProvider)
    {
        _clock = clock;
    }

    public async Task<List<UserPointTransaction>> ConsumeAsync(
        Guid userId,
        [ValueRange(int.MinValue, -1)] int amount,
        [CanBeNull] IEnumerable<Guid> accountIds = null,
        [CanBeNull] string entityType = null, [CanBeNull] string entityId = null,
        [CanBeNull] string remark = null,
        CancellationToken cancellationToken = default)
    {
        if (amount >= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be a negative number when consuming points.");
        }

        cancellationToken = GetCancellationToken(cancellationToken);

        // amount 为负数，表示需要扣多少
        // 例如：amount = -20

        // 取得所有仍未过期的积分记录（按过期时间升序）
        var userPointTransactions = await (await GetDbSetAsync())
            .Where(up => up.UserId == userId
                && up.AvailableAmount.HasValue
                && up.AvailableAmount.Value > 0
                && up.ConsumptionPriority.HasValue
                && up.ExpirationDate.HasValue
                && up.ExpirationDate > _clock.Now
                )
            .WhereIf(accountIds != null && accountIds.Any(), x => accountIds.Contains(x.AccountId))
            .OrderBy(up => up.ExpirationDate.Value)
            .ThenBy(up => up.ConsumptionPriority.Value)
            .ThenBy(up => up.CreationTime)
            .ToListAsync(cancellationToken);

        var newTransactions = new List<UserPointTransaction>();
        foreach (var userPointTransaction in userPointTransactions)
        {
            // 如果 amount = 0，表示本阶段已扣尽可扣分数  
            // 退出循环
            if (amount == 0)
                break;

            // remaining needed points as positive number
            var needed = -amount; // 例如：amount = -20 → needed = 20

            // 在本条记录中最多能扣多少
            var consumableAmount = Math.Min(needed, userPointTransaction.AvailableAmount.Value);

            // 扣减可用积分
            userPointTransaction.SetAvailableAmount(userPointTransaction.AvailableAmount.Value - consumableAmount);

            // 更新剩余 amount（仍为负数）
            amount += consumableAmount;
            // 例如：amount = -20 + 6 → -14  

            await UpdateAsync(userPointTransaction, cancellationToken: cancellationToken);

            if (newTransactions.Any(t => t.AccountId == userPointTransaction.AccountId))
            {
                var transaction = newTransactions.First(t=> t.AccountId == userPointTransaction.AccountId);
                transaction.SetAmount(transaction.Amount + userPointTransaction.Amount);
            }
            else
            {
                newTransactions.Add(new UserPointTransaction(
                    GuidGenerator.Create(),
                    userId,-consumableAmount,
                    UserPointTransactionType.Spend,
                    userPointTransaction.AccountId,
                    entityType,entityId,
                    remark,userPointTransaction.TenantId
                    ));
            }
        }

        return newTransactions;
    }


    public async Task<int> GetCountAsync(
        Guid userId,
        Guid? accountId = null,
        int? minAmount = null,
        int? maxAmount = null,
        DateTime? startExpirationTime = null,
        DateTime? endExpirationTime = null,
        string entityType = null,
        string entityId = null,
        CancellationToken cancellationToken = default)
    {
        return await(await GetQueryableAsync(userId, accountId, minAmount, maxAmount, startExpirationTime, endExpirationTime, entityType, entityId))
            .CountAsync(GetCancellationToken(cancellationToken));
    }

    public async Task<List<UserPointTransaction>> GetListAsync(
        Guid userId,
        Guid? accountId = null,
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
        var queryable = await GetQueryableAsync(userId, accountId, minAmount, maxAmount, startExpirationTime, endExpirationTime, entityType, entityId);
        queryable = queryable.OrderBy(sorting.IsNullOrEmpty() ? $"{nameof(UserPointTransaction.CreationTime)} desc" : sorting);
        return await queryable
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }


    protected virtual async Task<IQueryable<UserPointTransaction>> GetQueryableAsync(
        Guid userId,
        Guid? accountId = null,
        int? minAmount = null,
        int? maxAmount = null,
        DateTime? startExpirationTime = null,
        DateTime? endExpirationTime = null,
        string entityType = null,
        string entityId = null)
    {
        return (await GetDbSetAsync()).AsNoTracking().Where(up => up.UserId == userId)
            .WhereIf(accountId.HasValue, up => up.AccountId == accountId)
            .WhereIf(minAmount.HasValue, up => up.Amount >= minAmount.Value)
            .WhereIf(maxAmount.HasValue, up => up.Amount < maxAmount.Value)
            .WhereIf(startExpirationTime.HasValue, up => up.ExpirationDate.HasValue && up.ExpirationDate >= startExpirationTime.Value)
            .WhereIf(endExpirationTime.HasValue, up => up.ExpirationDate.HasValue && up.ExpirationDate < endExpirationTime.Value)
            .WhereIf(!entityType.IsNullOrEmpty(), up => up.EntityType == entityType)
            .WhereIf(!entityId.IsNullOrEmpty(), up => up.EntityId == entityId);
    }
}
