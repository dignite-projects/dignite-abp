using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Abp.TenantDomains.EntityFrameworkCore;

public class EfCoreTenantDomainRepository : EfCoreRepository<ITenantDomainsDbContext, TenantDomain, Guid>, ITenantDomainRepository
{
    public EfCoreTenantDomainRepository(
        IDbContextProvider<ITenantDomainsDbContext> dbContextProvider
        )
        : base(dbContextProvider)
    {
    }

    public async Task<TenantDomain?> FindByDomainNameAsync(string domainName, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync()).FirstOrDefaultAsync(td => td.DomainName == domainName, GetCancellationToken(cancellationToken));
    }

    public async Task<TenantDomain?> FindByTenantIdAsync(Guid? tenantId, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync()).FirstOrDefaultAsync(td => td.TenantId == tenantId, GetCancellationToken(cancellationToken));
    }

    public async Task<bool> DomainNameExistsAsync(string domainName, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync()).AnyAsync(td => td.DomainName == domainName, GetCancellationToken(cancellationToken));
    }
}
