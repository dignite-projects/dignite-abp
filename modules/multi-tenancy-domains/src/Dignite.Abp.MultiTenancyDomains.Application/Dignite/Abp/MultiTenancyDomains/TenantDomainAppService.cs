using System;
using System.Threading.Tasks;
using Dignite.Abp.MultiTenancyDomains.Localization;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.MultiTenancyDomains;

[Authorize(Permissions.MultiTenancyDomainsPermissions.GroupName)]
public class TenantDomainAppService : ApplicationService, ITenantDomainAppService
{
    private readonly ITenantDomainRepository _domainRepository;
    private readonly TenantDomainManager _domainManager;
    private readonly IDataFilter _dataFilter;

    public TenantDomainAppService(ITenantDomainRepository domainRepository, TenantDomainManager domainManager, IDataFilter dataFilter)
    {
        _domainRepository = domainRepository;
        _domainManager = domainManager;
        _dataFilter = dataFilter;

        LocalizationResource = typeof(MultiTenancyDomainsResource);
        ObjectMapperContext = typeof(AbpMultiTenancyDomainsApplicationModule);
    }

    [AllowAnonymous]
    public async Task<TenantDomainDto?> FindByDomainNameAsync(string domainName)
    {
        using (_dataFilter.Disable<IMultiTenant>())
        {
            var entity = await _domainRepository.FindByDomainNameAsync(domainName);

            return ObjectMapper.Map<TenantDomain, TenantDomainDto>(entity);
        }
    }

    public async Task<TenantDomainDto?> FindByCurrentTenantAsync()
    {
        var entity = await _domainRepository.FindByTenantIdAsync(CurrentTenant.Id);
        return ObjectMapper.Map<TenantDomain, TenantDomainDto>(entity);
    }

    public async Task<bool> DomainNameExistsAsync(string domainName)
    {
        var entity = await _domainRepository.FindByTenantIdAsync(CurrentTenant.Id);
        if (entity == null)
        {
            using (_dataFilter.Disable<IMultiTenant>())
            {
                return await _domainRepository.DomainNameExistsAsync(domainName);
            }
        }
        else
        {
            if (entity.DomainName.Equals(domainName, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            else
            {
                using (_dataFilter.Disable<IMultiTenant>())
                {
                    return await _domainRepository.DomainNameExistsAsync(domainName);
                }
            }
        }
    }


    [Authorize(Permissions.MultiTenancyDomainsPermissions.Update)]
    public async Task<TenantDomainDto> UpdateAsync(UpdateTenantDomainInput input)
    {
        var entity = await _domainRepository.FindByTenantIdAsync(CurrentTenant.Id);
        if (entity == null)
        {
            entity = await _domainManager.CreateAsync(input.DomainName, CurrentTenant.Id);
        }
        else
        {
            if (!entity.DomainName.Equals(input.DomainName, StringComparison.OrdinalIgnoreCase))
            {
                entity = await _domainManager.UpdateAsync(entity.Id, input.DomainName, entity.ConcurrencyStamp);
            }
        }
        return ObjectMapper.Map<TenantDomain, TenantDomainDto>(entity);
    }
}
