using JetBrains.Annotations;
using System.Collections.Generic;

namespace Dignite.CmsKit.Visits;

public class CmsKitVisitOptions
{
    [NotNull]
    public List<VisitEntityTypeDefinition> EntityTypes { get; } = new();
}
