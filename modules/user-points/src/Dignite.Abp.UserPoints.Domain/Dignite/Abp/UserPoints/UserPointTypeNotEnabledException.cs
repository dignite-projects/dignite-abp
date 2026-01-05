using System;
using Volo.Abp;

namespace Dignite.Abp.UserPoints;

/// <summary>
/// Exception thrown when the point type is unsupported.
/// </summary>
[Serializable]
public class UserPointTypeNotEnabledException : BusinessException
{
    public UserPointTypeNotEnabledException(string pointTypeName)
        : base(code: UserPointsErrorCodes.UserPoint.UserPointTypeNotEnabled)
    {
        PointTypeName = pointTypeName;
        WithData(nameof(UserPointAccount.PointTypeName), pointTypeName);
    }
    public string PointTypeName { get; }
}
