using System;
using System.Runtime.Serialization;
using Volo.Abp;

namespace Dignite.Abp.UserPoints;

/// <summary>
/// Exception thrown when the user does not have enough available points.
/// </summary>
[Serializable]
public class InsufficientAvailablePointsException : BusinessException
{
    public InsufficientAvailablePointsException(int points)
        : base(code: UserPointsErrorCodes.UserPointsItems.InsufficientAvailablePoints)
    {
        WithData(nameof(UserPointsItem.Points), points);
    }

}
