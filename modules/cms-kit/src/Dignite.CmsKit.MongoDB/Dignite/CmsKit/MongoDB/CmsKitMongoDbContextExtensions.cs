using Dignite.CmsKit.Favourites;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Dignite.CmsKit.MongoDB;

public static class CmsKitMongoDbContextExtensions
{
    public static void ConfigureCmsKit(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<Favourite>(x =>
        {
            x.CollectionName = DigniteCmsKitDbProperties.DbTablePrefix + "Favourites";
        });
    }
}
