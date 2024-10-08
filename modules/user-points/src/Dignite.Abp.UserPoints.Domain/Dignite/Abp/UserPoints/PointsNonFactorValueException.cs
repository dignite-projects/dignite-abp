using System;
using Volo.Abp;

namespace Dignite.Abp.UserPoints;

/// <summary>
/// The incoming points value is not a multiple of the factor value
/// </summary>
[Serializable]
public class PointsNonFactorValueException : BusinessException
{
    public PointsNonFactorValueException(int points,int factor)
        : base(code: UserPointsErrorCodes.UserPointsItems.PointsNonFactorValue)
    {
        WithData(nameof(UserPointsItem.Points), points);
        WithData(nameof(DignitePointsBlockOptions.Factor), factor);
    }

}
