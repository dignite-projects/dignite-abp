using System;
using JetBrains.Annotations;
using Volo.Abp;

namespace Dignite.Abp.UserPoints;

public class UserPointTypeDefinition : IEquatable<UserPointTypeDefinition>
{
    public UserPointTypeDefinition([NotNull] string pointType)
    {
        PointType = Check.NotNullOrEmpty(pointType, nameof(pointType));
    }

    [NotNull]
    public string PointType { get; protected set; }

    public bool Equals(UserPointTypeDefinition other)
    {
        return PointType == other?.PointType;
    }
}
