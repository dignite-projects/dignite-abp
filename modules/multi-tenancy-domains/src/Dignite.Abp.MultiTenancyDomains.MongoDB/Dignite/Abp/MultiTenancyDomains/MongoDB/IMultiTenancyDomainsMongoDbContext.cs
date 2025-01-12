using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.MultiTenancyDomains.MongoDB;

[ConnectionStringName(MultiTenancyDomainsDbProperties.ConnectionStringName)]
public interface IMultiTenancyDomainsMongoDbContext : IAbpMongoDbContext
{
    IMongoCollection<TenantDomain> TenantDomains { get; }
}
