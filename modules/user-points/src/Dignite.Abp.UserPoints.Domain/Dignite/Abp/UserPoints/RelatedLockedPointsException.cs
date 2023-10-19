using System;
using System.Runtime.Serialization;
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

    public RelatedLockedPointsException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {
    }
}
