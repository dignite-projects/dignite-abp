using AutoMapper;
using Dignite.CmsKit.Favourites;

namespace Dignite.CmsKit;
public class CmsKitDomainMappingProfile : Profile
{
    public CmsKitDomainMappingProfile()
    {
        CreateMap<Favourite, FavouriteEto>();
    }
}
