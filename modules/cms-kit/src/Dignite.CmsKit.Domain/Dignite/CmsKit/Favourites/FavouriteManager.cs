using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Dignite.CmsKit.Favourites;

public class FavouriteManager : DomainService
{
    protected IFavouriteRepository FavouriteRepository { get; }
    protected IFavouriteEntityTypeDefinitionStore FavouriteDefinitionStore { get; }

    public FavouriteManager(
        IFavouriteRepository favouriteRepository,
        IFavouriteEntityTypeDefinitionStore favouriteDefinitionStore)
    {
        FavouriteRepository = favouriteRepository;
        FavouriteDefinitionStore = favouriteDefinitionStore;
    }

    public async Task<Favourite> SetStarAsync(Guid userId, string entityType, string entityId)
    {
        var currentUserFavourite = await FavouriteRepository.GetCurrentUserFavouriteAsync(entityType, entityId, userId);

        if (currentUserFavourite != null)
        {
            return currentUserFavourite;
        }

        if (!await FavouriteDefinitionStore.IsDefinedAsync(entityType))
        {
            throw new EntityCantHaveFavouriteException(entityType);
        }

        return await FavouriteRepository.InsertAsync(
            new Favourite(
                GuidGenerator.Create(),
                entityType,
                entityId,
                userId,
                CurrentTenant.Id
            )
        );
    }
}
