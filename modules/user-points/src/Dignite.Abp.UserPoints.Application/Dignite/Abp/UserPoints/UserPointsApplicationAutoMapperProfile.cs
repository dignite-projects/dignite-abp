using AutoMapper;

namespace Dignite.Abp.UserPoints;

public class UserPointsApplicationAutoMapperProfile : Profile
{
    public UserPointsApplicationAutoMapperProfile()
    {
        CreateMap<UserPoint, UserPointDto>();
    }
}
