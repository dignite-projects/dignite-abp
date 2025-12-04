using System;
using JetBrains.Annotations;
using Volo.Abp;

namespace Dignite.Abp.UserPoints;

public class UserPointEntityTypeDefinition: IEquatable<UserPointEntityTypeDefinition>
{
    public UserPointEntityTypeDefinition([NotNull] string entityType)
    {
        EntityType = Check.NotNullOrEmpty(entityType, nameof(entityType));
    }

    [NotNull]
    public string EntityType { get; protected set; }

    public bool Equals(UserPointEntityTypeDefinition other)
    {
        return EntityType == other?.EntityType;
    }
}
