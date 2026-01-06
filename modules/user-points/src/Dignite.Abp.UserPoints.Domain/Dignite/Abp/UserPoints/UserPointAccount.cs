using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.UserPoints;

public class UserPointAccount: FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    protected UserPointAccount() { }

    public UserPointAccount(Guid id, Guid userId, string pointTypeName, int amount, DateTime lastTransactionTime, Guid? tenantId)
        :base(id)
    {
        UserId = userId;
        PointTypeName = pointTypeName;
        AddTotalEarned(amount, lastTransactionTime);
        Status = UserPointAccountStatus.Active;
        TenantId = tenantId;
    }

    /// <summary>
    /// Gets or sets the of the primary key of the user associated with this point
    /// </summary>
    public virtual Guid UserId { get; protected set; }

    public virtual string PointTypeName { get; protected set; }

    /// <summary>
    /// 当前可用余额（快照字段，避免每次聚合计算）
    /// </summary>
    public virtual int CurrentBalance { get; protected set; }

    /// <summary>
    /// 冻结余额（预留扣减、争议处理等）
    /// </summary>
    public virtual int FrozenBalance { get; set; }

    /// <summary>
    /// 累计获得（生命周期内总获得）
    /// </summary>
    public virtual int TotalEarned { get; protected set; } = 0;

    /// <summary>
    /// 累计消费（生命周期内总消费）
    /// </summary>
    public virtual int TotalSpent { get; protected set; } = 0;

    /// <summary>
    /// 累计过期（生命周期内总过期）
    /// </summary>
    public virtual int TotalExpired { get; set; } = 0;

    /// <summary>
    /// 最后交易时间
    /// </summary>
    public virtual DateTime LastTransactionTime { get; protected set; }

    /// <summary>
    /// 账户状态
    /// </summary>
    public virtual UserPointAccountStatus Status { get; protected set; }

    public virtual Guid? TenantId { get; protected set; }


    public virtual void AddFrozenBalance(int amount, DateTime lastTransactionTime)
    {
        if (amount > 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be a negative number when consuming points.");
        }
        CurrentBalance += amount;
        FrozenBalance += amount;
        LastModificationTime = lastTransactionTime;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="lastTransactionTime"></param>
    public virtual void AddTotalEarned(int amount, DateTime lastTransactionTime)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "When adding points, the amount must be a positive number.");
        }
        CurrentBalance += amount;
        TotalEarned += amount;
        LastModificationTime = lastTransactionTime;
    }

    public virtual void AddTotalSpent(int amount, DateTime lastTransactionTime)
    {
        if (amount > 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be a negative number when consuming points.");
        }
        CurrentBalance += amount;
        TotalSpent += amount;
        LastModificationTime = lastTransactionTime;
    }

    public virtual void AddTotalExpired(int amount, DateTime lastTransactionTime)
    {
        if (amount > 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be a negative number when consuming points.");
        }
        CurrentBalance += amount;
        TotalExpired += amount;
        LastModificationTime = lastTransactionTime;
    }

    public virtual void SetStatus(UserPointAccountStatus status)
    {
        Status = status;
    }
}
