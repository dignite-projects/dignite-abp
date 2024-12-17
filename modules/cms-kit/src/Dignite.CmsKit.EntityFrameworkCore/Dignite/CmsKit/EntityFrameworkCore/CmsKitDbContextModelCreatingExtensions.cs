using Dignite.CmsKit.GlobalFeatures;
using Dignite.CmsKit.Visits;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.GlobalFeatures;
using Volo.CmsKit;
using Volo.CmsKit.EntityFrameworkCore;

namespace Dignite.CmsKit.EntityFrameworkCore;

public static class CmsKitDbContextModelCreatingExtensions
{
    public static void ConfigureDigniteCmsKit(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.ConfigureCmsKit();

        if (GlobalFeatureManager.Instance.IsEnabled<VisitsFeature>())
        {
            builder.Entity<Visit>(r =>
            {
                r.ToTable(AbpCmsKitDbProperties.DbTablePrefix + "Visits", AbpCmsKitDbProperties.DbSchema);

                r.ConfigureByConvention();

                r.Property(x => x.EntityType).IsRequired().HasMaxLength(VisitConsts.MaxEntityTypeLength);
                r.Property(x => x.EntityId).IsRequired().HasMaxLength(VisitConsts.MaxEntityIdLength);
                r.Property(x => x.ClientIpAddress).HasMaxLength(VisitConsts.MaxClientIpAddressLength);
                r.Property(x => x.BrowserInfo).HasMaxLength(VisitConsts.MaxBrowserInfoLength);
                r.Property(x => x.DeviceInfo).HasMaxLength(VisitConsts.MaxDeviceInfoLength);

                r.HasIndex(x => new { x.TenantId, x.EntityType, x.CreatorId, x.EntityId, x.CreationTime });

                r.ApplyObjectExtensionMappings();
            });
        }
        else
        {
            builder.Ignore<Visit>();
        }

        builder.TryConfigureObjectExtensions<CmsKitDbContext>();
    }
}
