using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.MultiTenancyDomains.MongoDB;

[ConnectionStringName(MultiTenancyDomainsDbProperties.ConnectionStringName)]
public class MultiTenancyDomainsMongoDbContext : AbpMongoDbContext, IMultiTenancyDomainsMongoDbContext
{
    public IMongoCollection<TenantDomain> TenantDomains => Collection<TenantDomain>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureTenantDomains();
    }
}
