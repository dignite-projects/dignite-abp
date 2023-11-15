using Dignite.CmsKit.GlobalFeatures;
using Dignite.CmsKit.Favourites;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.GlobalFeatures;

namespace Dignite.CmsKit.EntityFrameworkCore;

public static class CmsKitDbContextModelCreatingExtensions
{
    public static void ConfigureDigniteCmsKit(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));


        if (GlobalFeatureManager.Instance.IsEnabled<FavouritesFeature>())
        {
            builder.Entity<Favourite>(r =>
            {
                r.ToTable(DigniteCmsKitDbProperties.DbTablePrefix + "Favourites", DigniteCmsKitDbProperties.DbSchema);

                r.ConfigureByConvention();

                r.Property(x => x.EntityType).IsRequired().HasMaxLength(FavouriteConsts.MaxEntityTypeLength);
                r.Property(x => x.EntityId).IsRequired().HasMaxLength(FavouriteConsts.MaxEntityIdLength);

                r.HasIndex(x => new { x.TenantId, x.EntityType, x.EntityId, x.CreatorId });

                r.ApplyObjectExtensionMappings();
            });
        }
        else
        {
            builder.Ignore<Favourite>();
        }

        builder.TryConfigureObjectExtensions<CmsKitDbContext>();
    }
}
