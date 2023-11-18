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

    public async Task<Favourite> CreateAsync(Guid userId, string entityType, string entityId)
    {
        if (!await FavouriteDefinitionStore.IsDefinedAsync(entityType))
        {
            throw new EntityCantHaveFavouriteException(entityType);
        }

        var favourite = await FavouriteRepository.GetCurrentUserAsync(entityType, entityId, userId);
        if (favourite != null)
        {
            return favourite;
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
