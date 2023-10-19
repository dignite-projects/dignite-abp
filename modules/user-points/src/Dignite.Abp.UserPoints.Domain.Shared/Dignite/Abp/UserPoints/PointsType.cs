namespace Dignite.Abp.UserPoints;

/// <summary>
/// The types of points are divided into two types: general points and specialized points.
/// When spending points, if you specify to spend general-purpose points, all general-purpose points can be used. 
/// If you specify that you want to spend special points, you can only use one special point.
/// </summary>
public enum PointsType
{
    /// <summary>
    /// 
    /// </summary>
    General,

    /// <summary>
    /// 
    /// </summary>
    Specialized
}
