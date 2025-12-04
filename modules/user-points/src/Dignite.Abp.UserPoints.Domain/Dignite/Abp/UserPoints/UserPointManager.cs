using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Services;

namespace Dignite.Abp.UserPoints;
public class UserPointManager: DomainService
{    
    protected IUserPointTypeDefinitionStore PointTypeDefinitionStore { get; }

    protected IUserPointEntityTypeDefinitionStore EntityTypeDefinitionStore { get; }

    protected IUserPointRepository UserPointsItemRepository { get; }

    public UserPointManager(IUserPointTypeDefinitionStore pointTypeDefinitionStore, IUserPointEntityTypeDefinitionStore entityTypeDefinitionStore,  IUserPointRepository userPointsItemRepository)
    {
        PointTypeDefinitionStore = pointTypeDefinitionStore;
        EntityTypeDefinitionStore = entityTypeDefinitionStore;
        UserPointsItemRepository = userPointsItemRepository;
    }

    public virtual async Task<UserPoint> CreateAsync(
        Guid userId, int amount, [NotNull] string pointType, DateTime? expirationTime = null,        
        [CanBeNull] string entityType = null, [CanBeNull] string entityId = null,
        [CanBeNull] string description = null
        )
    {
        if (!await PointTypeDefinitionStore.IsDefinedAsync(pointType))
        {
            throw new UnsupportedPointTypeException(pointType);
        }

        if (!entityType.IsNullOrEmpty() && !await EntityTypeDefinitionStore.IsDefinedAsync(entityType))
        {
            throw new EntityNotPointableException(entityType);
        }

        // 校准用户当前积分余额
        var latestPointRecord = await UserPointsItemRepository.CalibrateBalanceAsync(userId);
        var balance = CalculateBalance(amount, latestPointRecord);
        var nextExpirationAt = GetNextExpirationAt(expirationTime, latestPointRecord);

        // Creating User Point Object
        var userPoint = new UserPoint(
            GuidGenerator.Create(),
            userId, amount, pointType, expirationTime,
            entityType, entityId,
            description,
            balance, nextExpirationAt,
            CurrentTenant.Id);

        // returning
        return userPoint;
    }

    /// <summary>
    /// Calculates the new balance by applying the specified amount to the latest user point record.
    /// </summary>
    /// <param name="amount">The amount to add to the current balance. A positive value increases the balance; a negative value decreases it.</param>
    /// <param name="latestPointRecord">The most recent user point record containing the current balance. If null, the balance is assumed to be zero.</param>
    /// <returns>The updated balance after applying the specified amount.</returns>
    /// <exception cref="InsufficientPointException">Thrown when the amount to subtract exceeds the available balance.</exception>
    protected int CalculateBalance(int amount, UserPoint latestPointRecord)
    {
        var balance = latestPointRecord?.Balance ?? 0;
        if (amount < 0 && balance < Math.Abs(amount))
        {
            throw new InsufficientPointException(balance, amount); //余额不足
        }
        else
        {
            balance = balance + amount;
        }
        return balance;
    }

    /// <summary>
    /// Determines the next expiration time based on the provided expiration time and the latest user point record.
    /// </summary>
    /// <param name="expirationTime">The candidate expiration time to consider. If null, the expiration time from the latest point record is used.</param>
    /// <param name="latestPointRecord">The most recent user point record containing a potential expiration time. Can be null.</param>
    /// <returns>A DateTime value representing the next expiration time, or null if neither the provided expiration time nor the
    /// latest point record specify an expiration.</returns>
    protected DateTime? GetNextExpirationAt(DateTime? expirationTime, UserPoint latestPointRecord)
    {
        if (expirationTime.HasValue)
        {
            if (!latestPointRecord?.NextExpirationAt.HasValue ?? true)
            {
                return expirationTime;
            }
            else
            {
                return latestPointRecord.NextExpirationAt < expirationTime ? latestPointRecord.NextExpirationAt : expirationTime;
            }
        }
        return latestPointRecord?.NextExpirationAt;
    }
}
