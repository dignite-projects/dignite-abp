using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.UserPoints;

/// <summary>
/// User Points Record
/// </summary>
public class UserPointTransaction : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    /// <summary>
    /// 用于加分
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userId"></param>
    /// <param name="amount"></param>
    /// <param name="balance"></param>
    /// <param name="transactionType"></param>
    /// <param name="accountId"></param>
    /// <param name="consumptionPriority"></param>
    /// <param name="expirationDate"></param>
    /// <param name="entityType"></param>
    /// <param name="entityId"></param>
    /// <param name="remark"></param>
    /// <param name="tenantId"></param>
    public UserPointTransaction(Guid id, Guid userId, 
        [ValueRange(1, int.MaxValue)] int amount, int balance, 
        UserPointTransactionType transactionType, Guid accountId,
        int consumptionPriority, DateTime expirationDate, 
        string? entityType, string? entityId, 
        string? remark, Guid? tenantId)
        :base(id)
    {
        UserId = userId;
        Amount = amount;
        Balance = balance;
        TransactionType = transactionType;
        AccountId = accountId;
        ConsumptionPriority = consumptionPriority;
        ExpirationDate = expirationDate;
        EntityType = entityType;
        EntityId = entityId;
        Remark = remark;
        TenantId = tenantId;
        AvailableAmount = amount;
    }

    /// <summary>
    /// 用于减分
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userId"></param>
    /// <param name="amount"></param>
    /// <param name="transactionType"></param>
    /// <param name="accountId"></param>
    /// <param name="entityType"></param>
    /// <param name="entityId"></param>
    /// <param name="remark"></param>
    /// <param name="tenantId"></param>
    public UserPointTransaction(Guid id, Guid userId, 
        [ValueRange(int.MinValue, -1)] int amount, 
        UserPointTransactionType transactionType, Guid accountId, 
        string? entityType, string? entityId, 
        string? remark, Guid? tenantId)
        : base(id)
    {
        UserId = userId;
        Amount = amount;
        TransactionType = transactionType;
        AccountId = accountId;
        EntityType = entityType;
        EntityId = entityId;
        Remark = remark;
        TenantId = tenantId;
    }

    protected UserPointTransaction()
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
    /// Gets the available amount for the current entity, if specified.
    /// </summary>
    public virtual int? AvailableAmount { get; protected set; }

    /// <summary>
    /// Gets the current balance.
    /// </summary>
    public virtual int Balance { get; protected set; }

    /// <summary>
    /// 积分交易类型
    /// </summary>
    public virtual UserPointTransactionType TransactionType { get; protected set; }

    public virtual UserPointAccount Account { get; protected set; }

    public virtual Guid AccountId { get; protected set; }

    /// <summary>
    /// 消费优先级（数值越小越优先消费）
    /// 默认值：100
    /// 推荐范围：1-999
    /// </summary>
    public virtual int? ConsumptionPriority { get; protected set; }

    /// <summary>
    /// Getting or setting the expiration time of point
    /// </summary>
    public virtual DateTime? ExpirationDate { get; protected set; }

    /// <summary>
    /// Gets the type name of the entity associated with this instance.
    /// </summary>
    public virtual string? EntityType { get; protected set; }

    /// <summary>
    /// Gets the unique identifier for the entity.
    /// </summary>
    public virtual string? EntityId { get; protected set; }

    /// <summary>
    /// Gets the Remark associated with the current instance.
    /// </summary>
    public virtual string? Remark { get; protected set; }

    public virtual Guid? TenantId { get; protected set; }

    /// <summary>
    /// Sets the available amount for the current instance.
    /// </summary>
    /// <param name="availableAmount">The new available amount to assign. Must be a non-negative integer.</param>
    public virtual void SetAvailableAmount(int availableAmount)
    {
        AvailableAmount = availableAmount;
    }
    public virtual void SetAmount(int amount)
    {
        Amount = amount;
    }
    public virtual void SetBalance(int balance)
    {
        Balance = balance;
    }
}
