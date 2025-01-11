using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.TenantDomains.MongoDB;

public class MongoTenantDomainRepository : MongoDbRepository<ITenantDomainsMongoDbContext, TenantDomain, Guid>, ITenantDomainRepository
{
    public MongoTenantDomainRepository(IMongoDbContextProvider<ITenantDomainsMongoDbContext> dbContextProvider) : base(
        dbContextProvider)
    {
    }

    public async Task<bool> DomainNameExistsAsync(string domainName, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);

        return await (await GetMongoQueryableAsync(cancellationToken))
            .Where(td => td.DomainName == domainName)
            .AnyAsync(cancellationToken);
    }

    public async Task<TenantDomain> FindByDomainNameAsync(string domainName, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        return await (await GetMongoQueryableAsync(cancellationToken))
            .Where(td => td.DomainName == domainName)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TenantDomain> FindByTenantIdAsync(Guid? tenantId, CancellationToken cancellationToken = default)
    {
        cancellationToken = GetCancellationToken(cancellationToken);
        return await(await GetMongoQueryableAsync(cancellationToken))
            .Where(td => td.TenantId == tenantId)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
