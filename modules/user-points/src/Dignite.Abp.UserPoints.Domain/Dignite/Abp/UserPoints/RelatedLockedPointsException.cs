using System;
using Volo.Abp;

namespace Dignite.Abp.UserPoints;

/// <summary>
/// Exception with Locked-in Points
/// </summary>
[Serializable]
public class RelatedLockedPointsException : BusinessException
{
    public RelatedLockedPointsException(int points)
        : base(code: UserPointsErrorCodes.UserPointsItems.RelatedLockedPoints)
    {
        WithData(nameof(UserPointsItem.Points), points);
    }

}
