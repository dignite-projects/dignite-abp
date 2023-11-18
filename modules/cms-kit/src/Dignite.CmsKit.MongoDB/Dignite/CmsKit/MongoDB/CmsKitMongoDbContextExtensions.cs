using Dignite.CmsKit.Favourites;
using Dignite.CmsKit.Visits;
using Volo.Abp;
using Volo.Abp.MongoDB;
using Volo.CmsKit;

namespace Dignite.CmsKit.MongoDB;

public static class CmsKitMongoDbContextExtensions
{
    public static void DigniteConfigureCmsKit(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<Favourite>(x =>
        {
            x.CollectionName = AbpCmsKitDbProperties.DbTablePrefix + "Favourites";
        });

        builder.Entity<Visit>(x =>
        {
            x.CollectionName = AbpCmsKitDbProperties.DbTablePrefix + "Visits";
        });
    }
}
