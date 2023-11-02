using System;
using JetBrains.Annotations;

namespace Dignite.Abp.UserPoints;
public class GetUserTotalPointsInput
{
    public GetUserTotalPointsInput()
    {
    }

    public GetUserTotalPointsInput(DateTime? expirationDate)
    {
        ExpirationDate = expirationDate;
    }

    public GetUserTotalPointsInput(DateTime? expirationDate, string pointsDefinitionName, string pointsWorkflowName)
    {
        ExpirationDate = expirationDate;
        PointsDefinitionName = pointsDefinitionName;
        PointsWorkflowName = pointsWorkflowName;
    }

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
