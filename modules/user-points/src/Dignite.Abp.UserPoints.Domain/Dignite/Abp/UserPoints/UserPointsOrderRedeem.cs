using System;

namespace Dignite.Abp.UserPoints;

/// <summary>
/// Breakdown of points that make up a points order.
/// This class has no mapped database tables, stored as JSON in table fields
/// </summary>
public class UserPointsOrderRedeem
{
    public UserPointsOrderRedeem(
        string pointsDefinitionName,
        string pointsWorkflowName,
        PointsType pointsType, 
        int points, 
        DateTime expirationDate)
    {
        PointsDefinitionName = pointsDefinitionName;
        PointsWorkflowName = pointsWorkflowName;
        PointsType = pointsType;
        Points = points;
        ExpirationDate = expirationDate;
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
    /// Get or set the points of the points breakdown
    /// </summary>
    public int Points { get; set; }

    /// <summary>
    /// Getting or setting the expiration date of points.
    /// </summary>
    /// <remarks>
    /// Used to refund points and set the expiration time of the points.
    /// </remarks>
    public DateTime ExpirationDate { get; set; }
}
