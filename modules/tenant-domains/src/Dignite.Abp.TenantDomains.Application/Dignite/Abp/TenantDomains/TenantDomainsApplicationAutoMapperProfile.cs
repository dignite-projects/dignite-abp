using AutoMapper;

namespace Dignite.Abp.TenantDomains;

public class TenantDomainsApplicationAutoMapperProfile : Profile
{
    public TenantDomainsApplicationAutoMapperProfile()
    {
        CreateMap<TenantDomain, TenantDomainDto>();
    }
}
