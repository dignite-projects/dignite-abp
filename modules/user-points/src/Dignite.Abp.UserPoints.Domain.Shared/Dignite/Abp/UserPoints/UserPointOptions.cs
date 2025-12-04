using System.Collections.Generic;
using JetBrains.Annotations;

namespace Dignite.Abp.UserPoints;

public class UserPointOptions
{
    [NotNull]
    public List<UserPointEntityTypeDefinition> EntityTypes { get; } = new List<UserPointEntityTypeDefinition>();

    [NotNull]
    public List<UserPointTypeDefinition> PointTypes { get; } = new List<UserPointTypeDefinition>();
}
