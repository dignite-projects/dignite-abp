using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.MultiTenancyDomains.MongoDB;

public static class MultiTenancyDomainsMongoDbContextExtensions
{
    public static void ConfigureTenantDomains(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<TenantDomain>(x =>
        {
            x.CollectionName = MultiTenancyDomainsDbProperties.DbTablePrefix + "MultiTenancyDomains";
        });
    }
}
