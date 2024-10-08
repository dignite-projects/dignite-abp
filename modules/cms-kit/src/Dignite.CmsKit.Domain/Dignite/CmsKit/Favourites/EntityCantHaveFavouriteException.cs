using JetBrains.Annotations;
using Volo.Abp;

namespace Dignite.CmsKit.Favourites;

public class EntityCantHaveFavouriteException : BusinessException
{

    public EntityCantHaveFavouriteException([NotNull] string entityType)
    {
        Code = CmsKitErrorCodes.Favourites.EntityCantHaveFavourite;
        EntityType = Check.NotNullOrEmpty(entityType, nameof(entityType));
        WithData(nameof(EntityType), EntityType);
    }

    public string EntityType { get; }
}
