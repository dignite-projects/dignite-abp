using AutoMapper;
using Dignite.CmsKit.Public.Visits;
using Dignite.CmsKit.Visits;

namespace Dignite.CmsKit.Public;

public class CmsKitPublicApplicationAutoMapperProfile : Profile
{
    public CmsKitPublicApplicationAutoMapperProfile()
    {
        CreateMap<Visit, VisitDto>();
    }
}
