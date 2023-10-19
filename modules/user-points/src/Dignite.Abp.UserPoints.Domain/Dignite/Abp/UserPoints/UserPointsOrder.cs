using System;
using System.Collections.Generic;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.UserPoints;

/// <summary>
/// User Points Order
/// </summary>
public class UserPointsOrder : CreationAuditedAggregateRoot<Guid>, IDeletionAuditedObject, IMultiTenant
{
    protected UserPointsOrder()
    {
    }

    public UserPointsOrder(Guid id, int points, string businessOrderType, string businessOrderNumber, Guid userId, Guid? tenantId)
        :base(id)
    {
        Points = points;
        BusinessOrderType = businessOrderType;
        BusinessOrderNumber = businessOrderNumber;
        UserId = userId;
        TenantId = tenantId;
        Redeems = new List<UserPointsOrderRedeem>();
    }

    /// <summary>
    /// Getting or Setting Points
    /// </summary>
    /// <remarks>
    /// The value is equal to the sum of the points in <see cref="Redeems"/>
    /// </remarks>
    public virtual int Points { get; protected set; }

    /// <summary>
    /// Associated Points Redeems.
    /// </summary>
    /// <remarks>
    /// stored as JSON in table fields.
    /// This data is not normally exported to the customer and is only used when rolling back points to ensure that the original points have the same expiration date;
    /// </remarks>
    public virtual ICollection<UserPointsOrderRedeem> Redeems { get; protected set; }

    /// <summary>
    /// Get or set the business order type associated with a points order
    /// </summary>
    public virtual string BusinessOrderType { get; protected set; }

    /// <summary>
    /// Get or set the business order number associated with a points order
    /// </summary>
    public virtual string BusinessOrderNumber { get; protected set; }

    /// <summary>
    /// Gets or sets the of the primary key of the user associated with this order.
    /// </summary>
    public virtual Guid UserId { get; protected set; }

    public Guid? TenantId { get; set; }

    public Guid? DeleterId { get; set; }

    public DateTime? DeletionTime { get; set; }

    public bool IsDeleted { get; set; }
}
