using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.UserPoints;
public class GetUserPointsItemsInput: PagedResultRequestDto
{
    /// <summary>
    /// 
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime? EndTime { get; set; }

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
}
