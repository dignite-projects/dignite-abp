using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.TenantDomains.MongoDB;

public static class TenantDomainsMongoDbContextExtensions
{
    public static void ConfigureTenantDomains(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<TenantDomain>(x =>
        {
            x.CollectionName = TenantDomainsDbProperties.DbTablePrefix + "TenantDomains";
        });
    }
}
