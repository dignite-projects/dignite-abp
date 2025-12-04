using System;
using Volo.Abp;

namespace Dignite.Abp.UserPoints;

/// <summary>
/// Exception thrown when the point type is unsupported.
/// </summary>
[Serializable]
public class UnsupportedPointTypeException : BusinessException
{
    public UnsupportedPointTypeException(string pointType)
        : base(code: UserPointsErrorCodes.UserPoint.UnsupportedPointType)
    {
        PointType = pointType;
        WithData(nameof(UserPoint.PointType), pointType);
    }
    public string PointType { get; }
}
