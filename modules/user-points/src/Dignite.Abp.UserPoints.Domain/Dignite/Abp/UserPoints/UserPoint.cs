using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.UserPoints;

/// <summary>
/// User Points Record
/// </summary>
public class UserPoint : CreationAuditedAggregateRoot<Guid>, IMultiTenant
{
    public UserPoint(Guid id, Guid userId, int amount, string pointType, DateTime? expirationTime, string entityType, string entityId, string description, int balance, DateTime? nextExpirationAt,  Guid? tenantId)
        :base(id)
    {
        UserId = userId;
        Amount = amount;
        PointType = pointType;
        ExpirationTime = expirationTime;
        EntityType = entityType;
        EntityId = entityId;
        Description = description;
        Balance = balance;
        NextExpirationAt = nextExpirationAt;
        TenantId = tenantId;
    }

    protected UserPoint()
    {
    }

    /// <summary>
    /// Gets or sets the of the primary key of the user associated with this point
    /// </summary>
    public virtual Guid UserId { get; protected set; }

    /// <summary>
    /// Gets the amount associated with the current instance.
    /// </summary>
    public virtual int Amount { get; protected set; }

    /// <summary>
    /// Gets the name of the point type associated with this instance.
    /// </summary>
    public string PointType { get; protected set; }

    /// <summary>
    /// Getting or setting the expiration time of point
    /// </summary>
    public virtual DateTime? ExpirationTime { get; protected set; } = null;

    /// <summary>
    /// Gets the type name of the entity associated with this instance.
    /// </summary>
    public virtual string EntityType { get; protected set; }

    /// <summary>
    /// Gets the unique identifier for the entity.
    /// </summary>
    public virtual string EntityId { get; protected set; }

    /// <summary>
    /// Gets the description associated with the current instance.
    /// </summary>
    public virtual string? Description { get; protected set; }

    /// <summary>
    /// Gets the current balance.
    /// </summary>
    public virtual int Balance { get; protected set; }

    /// <summary>
    /// Gets the date and time when the next expiration is scheduled to occur, if available.
    /// </summary>
    /// <remarks>
    /// If a subsequent expiration time exists and is earlier than the current time, the user's points balance must be recalculated.
    /// </remarks>
    public DateTime? NextExpirationAt { get; protected set; }

    public Guid? TenantId { get; protected set; }

    /// <summary>
    /// Sets the current balance and the optional next expiration date.
    /// </summary>
    /// <param name="balance">The new balance value to assign. Must be a non-negative integer.</param>
    /// <param name="nextExpirationAt">The date and time when the balance will next expire, or null if the balance does not expire.</param>
    public virtual void SetBalance(int balance, DateTime? nextExpirationAt)
    {
        Balance = balance;
        NextExpirationAt = nextExpirationAt;
    }
}
