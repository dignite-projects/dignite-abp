using System;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.UserPoints;
public class GetUserPointsItemsInput: PagedResultRequestDto
{
    /// <summary>
    /// 
    /// </summary>
    [CanBeNull]
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [CanBeNull]
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [CanBeNull]
    public DateTime? ExpirationDate { get; set; }

    /// <summary>
    /// Gets or sets the of Points Definition Name.
    /// If <see cref="PointsDefinitionName"/> and <see cref="PointsWorkflowName"/> are both null, then query all generic points
    /// </summary>
    [CanBeNull]
    public virtual string PointsDefinitionName { get; set; }

    /// <summary>
    /// Gets or sets the of Points Workflow Name
    /// If <see cref="PointsDefinitionName"/> and <see cref="PointsWorkflowName"/> are both null, then query all generic points
    /// </summary>
    [CanBeNull]
    public virtual string PointsWorkflowName { get; set; }
}
