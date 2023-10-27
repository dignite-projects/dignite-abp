using System;

namespace Dignite.Abp.UserPoints;
public class GetUserTotalPointsInput
{
    public GetUserTotalPointsInput(DateTime? expirationDate)
    {
        ExpirationDate = expirationDate;
        PointsType = PointsType.General;
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
    public DateTime? ExpirationDate { get; set; }

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
