using JetBrains.Annotations;
using Volo.CmsKit;

namespace Dignite.CmsKit.Favourites;

public class FavouriteEntityTypeDefinition : EntityTypeDefinition
{
    public FavouriteEntityTypeDefinition(
        [NotNull] string entityType) : base(entityType)
    {
    }
}
