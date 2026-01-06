using AutoMapper;
using Volo.Abp.AutoMapper;

namespace Dignite.Abp.UserPoints;

public class UserPointsApplicationAutoMapperProfile : Profile
{
    public UserPointsApplicationAutoMapperProfile()
    {
        CreateMap<UserPointAccount, UserPointAccountDto>()
            .Ignore(upa=>upa.DisplayName);
        CreateMap<UserPointTransaction, UserPointTransactionDto>();
    }
}
