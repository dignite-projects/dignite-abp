using AutoMapper;
using Dignite.CmsKit.Public.Favourites;
using Dignite.CmsKit.Favourites;
using Dignite.CmsKit.Visits;
using Dignite.CmsKit.Public.Visits;

namespace Dignite.CmsKit.Public;

public class CmsKitPublicApplicationAutoMapperProfile : Profile
{
    public CmsKitPublicApplicationAutoMapperProfile()
    {
        CreateMap<Favourite, FavouriteDto>();
        CreateMap<Visit, VisitDto>();
    }
}
