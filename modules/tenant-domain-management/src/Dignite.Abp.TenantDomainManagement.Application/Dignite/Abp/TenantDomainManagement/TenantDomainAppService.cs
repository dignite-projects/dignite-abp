using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dignite.Abp.TenantDomain;
using Dignite.Abp.TenantDomainManagement.Settings;
using DnsClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Volo.Abp.Application.Services;
using Volo.Abp.SettingManagement;

namespace Dignite.Abp.TenantDomainManagement;

public class TenantDomainAppService(
    ISettingManager settingManager,
    IOptions<AbpTenantDomainManagementOptions> options,
    IWebServerManager webServerManager,
    IAuthServerRedirectUriManager authServerRedirectUriManager)
    : ApplicationService, ITenantDomainAppService
{

    private readonly AbpTenantDomainManagementOptions _options = options.Value;

    [Authorize(Permissions.TenantDomainManagementPermissions.ManageDomain)]
    public async Task<TenantDomainDto> ConnectAsync(ConnectTenantDomainInput input)
    {
        var isValid = await CheckCnameRecordAsync(input.DomainName);
        var tenantId = CurrentTenant.Id!.Value;
        var tenantName = CurrentTenant.Name!;

        if (isValid)
        {
            //添加域名记录到数据库
            await settingManager.SetForTenantAsync(tenantId, TenantDomainSettingNames.DomainName, input.DomainName);

            //将域名绑定到站点
            await webServerManager.AddOrUpdateDomainAsync(input.DomainName, _options.ProxyAddress, tenantId);

            //将域名添加到授权应用的RedirectUris 和 PostLogoutRedirectUris 中
            await authServerRedirectUriManager.AddRedirectDomainAsync(_options.AuthServerClientId, input.DomainName);
        }

        //
        return new TenantDomainDto(input.DomainName, isValid, _options.GetTenantDomain(tenantName), tenantId);
    }

    public async Task<TenantDomainDto?> GetAsync()
    {
        var domainName = await SettingProvider.GetOrNullAsync(TenantDomainSettingNames.DomainName);
        bool isValid = domainName == null? false : await CheckCnameRecordAsync(domainName);
        return new TenantDomainDto(domainName, isValid, _options.GetTenantDomain(CurrentTenant.Name), CurrentTenant.Id);
    }

    [Authorize(Permissions.TenantDomainManagementPermissions.ManageDomain)]
    public async Task<bool> CheckCnameRecordAsync(string domainName)
    {
        var expectedCname = _options.GetTenantDomain(CurrentTenant.Name!);
        var lookup = new LookupClient();
        var result = await lookup.QueryAsync(domainName, QueryType.CNAME);

        var cnameRecord = result.Answers.CnameRecords().FirstOrDefault();
        if (cnameRecord != null)
        {
            return cnameRecord.CanonicalName.Value.TrimEnd('.').Equals(expectedCname, System.StringComparison.OrdinalIgnoreCase);
        }
        return false;
    }
}
