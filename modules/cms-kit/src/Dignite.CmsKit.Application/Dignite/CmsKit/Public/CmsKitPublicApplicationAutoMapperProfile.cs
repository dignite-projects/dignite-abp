using AutoMapper;
using Dignite.CmsKit.Public.Favourites;
using Dignite.CmsKit.Favourites;

namespace Dignite.CmsKit.Public;

public class CmsKitPublicApplicationAutoMapperProfile : Profile
{
    public CmsKitPublicApplicationAutoMapperProfile()
    {
        CreateMap<Favourite, FavouriteDto>();
    }
}
