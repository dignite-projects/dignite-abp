using System.Linq;
using System.Threading.Tasks;
using Dignite.Abp.TenantDomainManagement.Settings;
using Dignite.Abp.TenantDomain;
using DnsClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Volo.Abp.Application.Services;
using Volo.Abp.SettingManagement;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Dignite.Abp.TenantDomainManagement;

public class TenantDomainAppService : ApplicationService, ITenantDomainAppService
{
    private readonly ISettingManager _settingManager;
    private readonly AbpTenantDomainManagementOptions _options;
    private readonly IWebServerManager _webServerManager;
    private readonly IAuthServerRedirectUriManager _redirectUriManager;

    public TenantDomainAppService(ISettingManager settingManager, IOptions<AbpTenantDomainManagementOptions> options, IWebServerManager webServerManager, IAuthServerRedirectUriManager authServerRedirectUriManager)
    {
        _settingManager = settingManager;
        _options = options.Value;
        _webServerManager = webServerManager;
        _redirectUriManager = authServerRedirectUriManager;
    }

    [Authorize(Permissions.TenantDomainManagementPermissions.ManageDomain)]
    public async Task<TenantDomainDto> ConnectAsync(ConnectTenantDomainInput input)
    {
        bool isValid = await CheckCnameRecordAsync(input.DomainName);
        if (isValid)
        {
            //添加域名记录到数据库
            await _settingManager.SetForTenantAsync(CurrentTenant.Id.Value, TenantDomainSettingNames.DomainName, input.DomainName);

            //将域名绑定到站点
            await _webServerManager.AddOrUpdateDomainAsync(input.DomainName, _options.ProxyAddress, CurrentTenant.Id.Value, _options.WebServerSiteName);

            //将域名添加到授权应用的RedirectUris 和 PostLogoutRedirectUris 中
            await _redirectUriManager.AddRedirectDomainAsync(_options.AuthServerClientId, input.DomainName);

            //
            return new TenantDomainDto(input.DomainName, true, _options.GetTenantDomain(CurrentTenant.Name), CurrentTenant.Id);
        }
        else
        {
            return new TenantDomainDto(input.DomainName, false, _options.GetTenantDomain(CurrentTenant.Name), CurrentTenant.Id);
        }
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
        #if DEBUG
                return true;
        #endif

        string expectedCname = _options.GetTenantDomain(CurrentTenant.Name);
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
