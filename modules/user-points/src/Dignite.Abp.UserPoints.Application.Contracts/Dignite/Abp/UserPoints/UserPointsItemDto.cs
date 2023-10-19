using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.UserPoints;

[Serializable]
public class UserPointsItemDto: ExtensibleCreationAuditedEntityDto<Guid>
{
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
}
