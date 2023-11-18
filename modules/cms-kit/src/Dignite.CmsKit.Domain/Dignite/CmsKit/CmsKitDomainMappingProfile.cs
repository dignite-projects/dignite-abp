using AutoMapper;
using Dignite.CmsKit.Favourites;
using Dignite.CmsKit.Visits;

namespace Dignite.CmsKit;
public class CmsKitDomainMappingProfile : Profile
{
    public CmsKitDomainMappingProfile()
    {
        CreateMap<Favourite, FavouriteEto>();
        CreateMap<Visit, VisitEto>();
    }
}
