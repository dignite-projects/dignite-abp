using System;
using System.Collections.Generic;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.UserPoints;

/// <summary>
/// User Points Item
/// </summary>
public class UserPointsItem : CreationAuditedAggregateRoot<Guid>,IDeletionAuditedObject, IMultiTenant
{
    public UserPointsItem(
        Guid id, 
        string pointsDefinitionName, 
        string pointsWorkflowName, 
        PointsType pointsType, 
        int points, 
        DateTime expirationDate, 
        Guid userId, 
        Guid? tenantId)
        :base(id)
    {
        PointsDefinitionName = pointsDefinitionName;
        PointsWorkflowName = pointsWorkflowName;
        PointsType = pointsType;
        Points = points;
        ExpirationDate = expirationDate;
        UserId = userId;
        TenantId = tenantId;
    }

    protected UserPointsItem()
    {
    }

    /// <summary>
    /// Gets or sets the of <see cref="Abp.Points.PointsDefinition.Name"/>
    /// </summary>
    public virtual string PointsDefinitionName { get; protected set; }

    /// <summary>
    /// Gets or sets the of <see cref="Abp.Points.PointsWorkflow.Name"/>
    /// </summary>
    public virtual string PointsWorkflowName { get; protected set; }

    /// <summary>
    /// The types of points are divided into two types: general points and specialized points.
    /// The default value is <see cref="PointsType.General"/>.
    /// </summary>
    /// <remarks>
    /// General points can be used for any;
    /// Specialized points can be used for specified orders;
    /// </remarks>
    public virtual PointsType PointsType { get; protected set; } = PointsType.General;

    /// <summary>
    /// Getting or Setting Points
    /// </summary>
    /// <remarks>
    /// The points value must be a multiple of the factor
    /// </remarks>
    public virtual int Points { get; protected set; }

    /// <summary>
    /// Getting or setting the expiration date of points
    /// </summary>
    public virtual DateTime ExpirationDate { get; protected set; }

    /// <summary>
    /// Gets or sets the of the primary key of the user associated with this points.
    /// </summary>
    public virtual Guid UserId { get; protected set; }

    public Guid? TenantId { get; set; }

    public Guid? DeleterId { get; set; }

    public DateTime? DeletionTime { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<UserPointsBlock> PointsBlocks { get; protected set; }
}
