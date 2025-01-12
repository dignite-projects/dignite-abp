using System.Threading.Tasks;
using Dignite.Abp.MultiTenancyDomains.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Abp.MultiTenancyDomains;

[Area(MultiTenancyDomainsRemoteServiceConsts.ModuleName)]
[RemoteService(Name = MultiTenancyDomainsRemoteServiceConsts.RemoteServiceName)]
[Route("api/multi-tenancy-domains/tenant-domain")]
public class TenantDomainController : AbpControllerBase, ITenantDomainAppService
{
    private readonly ITenantDomainAppService _tenantDomainAppService;

    public TenantDomainController(ITenantDomainAppService tenantDomainAppService)
    {
        _tenantDomainAppService = tenantDomainAppService;
        LocalizationResource = typeof(MultiTenancyDomainsResource);
    }

    [HttpGet]
    [Route("domain-name-exists")]
    public async Task<bool> DomainNameExistsAsync(string domainName)
    {
        return await _tenantDomainAppService.DomainNameExistsAsync(domainName);
    }

    [HttpGet]
    [Route("find-by-current-tenant")]
    public async Task<TenantDomainDto?> FindByCurrentTenantAsync()
    {
        return await _tenantDomainAppService.FindByCurrentTenantAsync();
    }

    [HttpGet]
    [Route("find-by-domain-name")]
    public async Task<TenantDomainDto?> FindByDomainNameAsync(string domainName)
    {
        return await _tenantDomainAppService.FindByDomainNameAsync(domainName);
    }

    [HttpPut]
    public async Task<TenantDomainDto> UpdateAsync(UpdateTenantDomainInput input)
    {
        return await _tenantDomainAppService.UpdateAsync(input);
    }
}
