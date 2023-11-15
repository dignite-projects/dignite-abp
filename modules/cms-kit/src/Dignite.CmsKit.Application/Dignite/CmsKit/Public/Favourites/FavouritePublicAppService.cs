using Dignite.CmsKit.Features;
using Dignite.CmsKit.GlobalFeatures;
using Dignite.CmsKit.Favourites;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Volo.Abp.Authorization;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.Users;

namespace Dignite.CmsKit.Public.Favourites;

[RequiresFeature(CmsKitFeatures.FavouriteEnable)]
[RequiresGlobalFeature(typeof(FavouritesFeature))]
public class FavouritePublicAppService : CmsKitPublicAppServiceBase, IFavouritePublicAppService
{
    protected IFavouriteRepository FavouriteRepository { get; }
    protected FavouriteManager FavouriteManager { get; }

    public FavouritePublicAppService(
        IFavouriteRepository favouriteRepository,
        FavouriteManager favouriteManager)
    {
        FavouriteRepository = favouriteRepository;
        FavouriteManager = favouriteManager;
    }

    [Authorize]
    public virtual async Task<FavouriteDto> CreateAsync(string entityType, string entityId)
    {
        var userId = CurrentUser.GetId();

        var favourite = await FavouriteManager.SetStarAsync(userId, entityType, entityId);

        return ObjectMapper.Map<Favourite, FavouriteDto>(favourite);
    }

    [Authorize]
    public virtual async Task DeleteAsync(string entityType, string entityId)
    {
        var favourite = await FavouriteRepository.GetCurrentUserFavouriteAsync(entityType, entityId, CurrentUser.GetId());

        if (favourite.CreatorId != CurrentUser.GetId())
        {
            throw new AbpAuthorizationException();
        }

        await FavouriteRepository.DeleteAsync(favourite.Id);
    }
}
