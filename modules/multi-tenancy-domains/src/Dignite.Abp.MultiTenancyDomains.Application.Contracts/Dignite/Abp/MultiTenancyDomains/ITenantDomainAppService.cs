using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Dignite.Abp.MultiTenancyDomains;

public interface ITenantDomainAppService : IApplicationService
{
    Task<TenantDomainDto?> FindByDomainNameAsync(string domainName);

    Task<bool> DomainNameExistsAsync(string domainName);

    Task<TenantDomainDto?> FindByCurrentTenantAsync();

    Task<TenantDomainDto> UpdateAsync(UpdateTenantDomainInput input);
}
