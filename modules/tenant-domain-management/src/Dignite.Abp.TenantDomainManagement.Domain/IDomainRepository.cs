using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Abp.TenantDomainManagement;
public interface IDomainRepository : IBasicRepository<Domain, Guid>
{
    Task<Domain> FindByNameAsync(
        string name,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);
}
