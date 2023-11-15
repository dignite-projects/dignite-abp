using Dignite.CmsKit.Features;
using Dignite.CmsKit.GlobalFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;

namespace Dignite.CmsKit.Public.Favourites;

[RequiresFeature(CmsKitFeatures.FavouriteEnable)]
[RequiresGlobalFeature(typeof(FavouritesFeature))]
[RemoteService(Name = DigniteCmsKitPublicRemoteServiceConsts.RemoteServiceName)]
[Area(DigniteCmsKitPublicRemoteServiceConsts.ModuleName)]
[Route("api/cms-kit-public/favourites")]
public class FavouritePublicController : CmsKitPublicControllerBase, IFavouritePublicAppService
{
    protected IFavouritePublicAppService FavouritePublicAppService { get; }

    public FavouritePublicController(IFavouritePublicAppService favouritePublicAppService)
    {
        FavouritePublicAppService = favouritePublicAppService;
    }

    [HttpPut]
    [Route("{entityType}/{entityId}")]
    [Authorize]
    public virtual Task<FavouriteDto> CreateAsync(string entityType, string entityId)
    {
        return FavouritePublicAppService.CreateAsync(entityType, entityId);
    }

    [HttpDelete]
    [Route("{entityType}/{entityId}")]
    [Authorize]
    public virtual Task DeleteAsync(string entityType, string entityId)
    {
        return FavouritePublicAppService.DeleteAsync(entityType, entityId);
    }
}
