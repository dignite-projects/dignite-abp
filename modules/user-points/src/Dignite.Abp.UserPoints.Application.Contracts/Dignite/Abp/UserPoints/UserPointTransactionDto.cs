using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.UserPoints;

[Serializable]
public class UserPointTransactionDto: ExtensibleCreationAuditedEntityDto<Guid>
{

    /// <summary>
    /// Gets or sets the of the primary key of the user associated with this point
    /// </summary>
    public virtual Guid UserId { get; set; }

    /// <summary>
    /// Gets the amount associated with the current instance.
    /// </summary>
    public virtual int Amount { get; set; }

    /// <summary>
    /// Gets the available amount for the current entity, if specified.
    /// </summary>
    public virtual int? AvailableAmount { get; set; }

    /// <summary>
    /// Gets the current balance.
    /// </summary>
    public virtual int Balance { get; set; }

    /// <summary>
    /// 积分交易类型
    /// </summary>
    public UserPointTransactionType TransactionType { get; protected set; }

    /// <summary>
    /// Gets the name of the point type associated with this instance.
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// 消费优先级（数值越小越优先消费）
    /// 默认值：100
    /// 推荐范围：1-999
    /// </summary>
    public int? ConsumptionPriority { get; set; }

    /// <summary>
    /// Getting or setting the expiration time of point
    /// </summary>
    public virtual DateTime? ExpirationDate { get; set; }

    /// <summary>
    /// Gets the type name of the entity associated with this instance.
    /// </summary>
    public virtual string EntityType { get; set; }

    /// <summary>
    /// Gets the unique identifier for the entity.
    /// </summary>
    public virtual string EntityId { get; set; }

    /// <summary>
    /// Gets the description associated with the current instance.
    /// </summary>
    public virtual string? Remark { get; set; }
}
