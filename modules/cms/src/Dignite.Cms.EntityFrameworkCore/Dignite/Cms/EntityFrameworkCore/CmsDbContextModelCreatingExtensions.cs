using Dignite.Abp.DynamicForms;
using Dignite.Cms.Entries;
using Dignite.Cms.Fields;
using Dignite.Cms.Sections;
using Volo.CmsKit.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.EntityFrameworkCore.ValueConverters;
using Dignite.FileExplorer.EntityFrameworkCore;

namespace Dignite.Cms.EntityFrameworkCore;

public static class CmsDbContextModelCreatingExtensions
{
    public static void ConfigureCms(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.ConfigureCmsKit();
        builder.ConfigureFileExplorer();


        builder.Ignore<EntryFieldTab>();
		builder.Ignore<EntryField>();

        builder.Entity<Section>(section =>
        {
            //Configure table & schema name
            section.ToTable(CmsDbProperties.DbTablePrefix + "Sections", CmsDbProperties.DbSchema);

            section.ConfigureByConvention();

            //Properties
            section.Property(s => s.DisplayName).IsRequired().HasMaxLength(SectionConsts.MaxDisplayNameLength);
            section.Property(s => s.Name).IsRequired().HasMaxLength(SectionConsts.MaxNameLength);
            section.Property(r => r.Route).IsRequired().HasMaxLength(SectionConsts.MaxPageRouteLength);
            section.Property(r => r.Template).IsRequired().HasMaxLength(SectionConsts.MaxPagetemplateLength);

            //Relations
            section.HasMany(s => s.EntryTypes).WithOne().HasForeignKey(et => et.SectionId);
            section.HasMany(s => s.Entries).WithOne().HasForeignKey(e => e.SectionId);

            //Indexs
            section.HasIndex(s => s.Name);
        });

        builder.Entity<EntryType>(entryType =>
        {
            //Configure table & schema name
            entryType.ToTable(CmsDbProperties.DbTablePrefix + "EntryTypes", CmsDbProperties.DbSchema);

            entryType.ConfigureByConvention();

            //Properties
            entryType.Property(et => et.DisplayName).IsRequired().HasMaxLength(EntryTypeConsts.MaxDisplayNameLength);
            entryType.Property(et => et.Name).IsRequired().HasMaxLength(EntryTypeConsts.MaxNameLength);
            entryType.Property(et => et.FieldTabs).HasConversion(new AbpJsonValueConverter<ICollection<EntryFieldTab>>());                

            //
            entryType.HasIndex(et=>et.SectionId);
        });

        builder.Entity<FieldGroup>(fieldGroup =>
        {
            //Configure table & schema name
            fieldGroup.ToTable(CmsDbProperties.DbTablePrefix + "FieldGroups", CmsDbProperties.DbSchema);

            fieldGroup.ConfigureByConvention();

            //Properties
            fieldGroup.Property(fg => fg.Name).IsRequired().HasMaxLength(FieldGroupConsts.MaxNameLength);
            fieldGroup.HasMany(fg => fg.Fields).WithOne().HasForeignKey(e => e.GroupId).OnDelete(DeleteBehavior.SetNull);
        });

        builder.Entity<Field>(field =>
        {
            //Configure table & schema name
            field.ToTable(CmsDbProperties.DbTablePrefix + "Fields", CmsDbProperties.DbSchema);

            field.ConfigureByConvention();

            field.Property(f=>f.DisplayName).IsRequired().HasMaxLength(FieldConsts.MaxDisplayNameLength);
            field.Property(f => f.Name).IsRequired().HasMaxLength(FieldConsts.MaxNameLength);
            field.Property(f => f.FormControlName).IsRequired().HasMaxLength(FieldConsts.MaxFormControlNameLength);
            field.Property(f => f.Description).HasMaxLength(FieldConsts.MaxDescriptionLength);
            field.Property(et => et.FormConfiguration).HasConversion(new AbpJsonValueConverter<FormConfigurationDictionary>());
        });

        builder.Entity<Entry>(entry =>
        {
            //Configure table & schema name
            entry.ToTable(CmsDbProperties.DbTablePrefix + "Entries", CmsDbProperties.DbSchema);

            entry.ConfigureByConvention();

            entry.Property(e => e.Culture).IsRequired().HasMaxLength(EntryConsts.MaxLanguageCultureNameLength);
            entry.Property(e => e.Title).IsRequired().HasMaxLength(EntryConsts.MaxTitleLength);
            entry.Property(e => e.Slug).IsRequired().HasMaxLength(EntryConsts.MaxSlugLength);
            entry.Property(e => e.VersionNotes).HasMaxLength(EntryConsts.MaxRevisionNotesLength);

            //Indexes
            entry.HasIndex(e => new { e.Culture, e.SectionId, e.PublishTime, e.Status });
            entry.HasIndex(e => new { e.CreatorId, e.SectionId, e.PublishTime, e.Status });
            entry.HasIndex(e => new { e.Culture, e.SectionId, e.Slug });
        });

        builder.TryConfigureObjectExtensions<CmsDbContext>();
    }
}
