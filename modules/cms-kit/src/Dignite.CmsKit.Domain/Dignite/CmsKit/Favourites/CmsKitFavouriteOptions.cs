using JetBrains.Annotations;
using System.Collections.Generic;

namespace Dignite.CmsKit.Favourites;

public class CmsKitFavouriteOptions
{
    [NotNull]
    public List<FavouriteEntityTypeDefinition> EntityTypes { get; } = new();
}
