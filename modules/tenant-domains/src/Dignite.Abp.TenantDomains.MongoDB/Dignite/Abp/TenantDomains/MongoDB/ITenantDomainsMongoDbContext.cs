using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.TenantDomains.MongoDB;

[ConnectionStringName(TenantDomainsDbProperties.ConnectionStringName)]
public interface ITenantDomainsMongoDbContext : IAbpMongoDbContext
{
    IMongoCollection<TenantDomain> TenantDomains { get; }
}
