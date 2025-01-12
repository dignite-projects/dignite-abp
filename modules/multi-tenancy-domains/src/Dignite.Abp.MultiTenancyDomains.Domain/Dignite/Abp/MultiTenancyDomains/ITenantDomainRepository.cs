using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Abp.MultiTenancyDomains;

public interface ITenantDomainRepository : IBasicRepository<TenantDomain, Guid>
{
    Task<bool> DomainNameExistsAsync(string domainName, CancellationToken cancellationToken = default);

    Task<TenantDomain> FindByDomainNameAsync(string domainName, CancellationToken cancellationToken = default);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TenantDomain> FindByTenantIdAsync(Guid? tenantId, CancellationToken cancellationToken = default);
}
