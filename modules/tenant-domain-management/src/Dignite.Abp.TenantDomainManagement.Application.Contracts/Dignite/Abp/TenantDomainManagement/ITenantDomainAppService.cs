using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.TenantDomainManagement;

public interface ITenantDomainAppService : IApplicationService
{
    Task<TenantDomainDto> ConnectAsync(ConnectTenantDomainInput input);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<TenantDomainDto?> GetAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="domainName"></param>
    /// <returns></returns>
    Task<bool> CheckCnameRecordAsync(string domainName);
}
