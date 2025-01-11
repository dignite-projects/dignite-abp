using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.TenantDomains.MongoDB;

[ConnectionStringName(TenantDomainsDbProperties.ConnectionStringName)]
public class TenantDomainsMongoDbContext : AbpMongoDbContext, ITenantDomainsMongoDbContext
{
    public IMongoCollection<TenantDomain> TenantDomains => Collection<TenantDomain>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureTenantDomains();
    }
}
