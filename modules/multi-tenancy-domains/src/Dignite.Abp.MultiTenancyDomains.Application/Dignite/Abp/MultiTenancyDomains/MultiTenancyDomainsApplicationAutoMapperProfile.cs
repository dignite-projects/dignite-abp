using AutoMapper;

namespace Dignite.Abp.MultiTenancyDomains;

public class MultiTenancyDomainsApplicationAutoMapperProfile : Profile
{
    public MultiTenancyDomainsApplicationAutoMapperProfile()
    {
        CreateMap<TenantDomain, TenantDomainDto>();
    }
}
