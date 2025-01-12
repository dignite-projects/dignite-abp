using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.MultiTenancyDomains;

public class TenantDomainManager : DomainService
{
    private readonly ITenantDomainRepository _domainRepository;
    private readonly IDataFilter _dataFilter;

    public TenantDomainManager(ITenantDomainRepository domainRepository, IDataFilter dataFilter)
    {
        _domainRepository = domainRepository;
        _dataFilter = dataFilter;
    }

    public async Task<TenantDomain> CreateAsync(string domainName, Guid? tenantId)
    {
        await CheckDomainNameExistenceAsync(domainName);

        var domain = new TenantDomain(GuidGenerator.Create(), domainName, tenantId);
        return await _domainRepository.InsertAsync(domain);
    }

    public async Task<TenantDomain> UpdateAsync(Guid id, string domainName, string concurrencyStamp)
    {
        var entity = await _domainRepository.GetAsync(id, false);
        entity.SetConcurrencyStampIfNotNull(concurrencyStamp);
        if (!entity.DomainName.Equals(domainName, StringComparison.OrdinalIgnoreCase))
        {
            await CheckDomainNameExistenceAsync(domainName);
        }

        //
        entity.DomainName = domainName;
        return await _domainRepository.UpdateAsync(entity);
    }

    protected virtual async Task CheckDomainNameExistenceAsync(string domainName)
    {
        using (_dataFilter.Disable<IMultiTenant>())
        {
            if (await _domainRepository.DomainNameExistsAsync(domainName))
            {
                throw new DomainNameAlreadyExistException(domainName);
            }
        }
    }
}
