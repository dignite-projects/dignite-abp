using System.Threading.Tasks;
using Dignite.Abp.TenantDomainManagement.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Abp.TenantDomainManagement;

[Area(TenantDomainManagementRemoteServiceConsts.ModuleName)]
[RemoteService(Name = TenantDomainManagementRemoteServiceConsts.RemoteServiceName)]
[Route("api/tenant-domain-management/tenant-domain")]
public class TenantDomainController : AbpControllerBase, ITenantDomainAppService
{
    private readonly ITenantDomainAppService _tenantDomainAppService;

    public TenantDomainController(ITenantDomainAppService tenantDomainAppService)
    {
        _tenantDomainAppService = tenantDomainAppService;
        LocalizationResource = typeof(TenantDomainManagementResource);
    }

    [HttpPost]
    [Route("connect")]
    public async Task<TenantDomainDto> ConnectAsync(ConnectTenantDomainInput input)
    {
        return await _tenantDomainAppService.ConnectAsync(input);
    }

    [HttpGet]
    public async Task<TenantDomainDto?> GetAsync()
    {
        return await _tenantDomainAppService.GetAsync();
    }

    [HttpGet]
    [Route("check-cname-record")]
    public async Task<bool> CheckCnameRecordAsync(string domainName)
    {
        return await _tenantDomainAppService.CheckCnameRecordAsync(domainName);
    }
}
