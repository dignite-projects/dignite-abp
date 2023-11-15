using JetBrains.Annotations;

namespace Dignite.CmsKit.Favourites;

public class FavouriteEntityTypeDefinition : EntityTypeDefinition
{
    public FavouriteEntityTypeDefinition(
        [NotNull] string entityType) : base(entityType)
    {
    }
}
