using JetBrains.Annotations;
using Volo.CmsKit;

namespace Dignite.CmsKit.Visits;

public class VisitEntityTypeDefinition : EntityTypeDefinition
{
    public VisitEntityTypeDefinition(
        [NotNull] string entityType) : base(entityType)
    {
    }
}
