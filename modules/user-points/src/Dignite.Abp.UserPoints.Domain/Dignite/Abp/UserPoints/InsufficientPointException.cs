using System;
using Volo.Abp;

namespace Dignite.Abp.UserPoints;

/// <summary>
/// Exception thrown when the user does not have enough available points.
/// </summary>
[Serializable]
public class InsufficientPointException : BusinessException
{
    public InsufficientPointException(int balance, int amount)
        : base(code: UserPointsErrorCodes.UserPoint.InsufficientPoint)
    {
        WithData(nameof(UserPoint.Balance), balance);
        WithData(nameof(UserPoint.Amount), amount);
    }
}
