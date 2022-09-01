using System;
using Dignite.Abp.FileManagement.Files;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Dignite.Abp.FileManagement.EntityFrameworkCore;

public static class FileManagementDbContextModelCreatingExtensions
{
    public static void ConfigureFileManagement(
        this ModelBuilder builder,
        Action<FileManagementModelBuilderConfigurationOptions> optionsAction = null)
    {
        Check.NotNull(builder, nameof(builder));

        var options = new FileManagementModelBuilderConfigurationOptions(
            FileManagementDbProperties.DbTablePrefix,
            FileManagementDbProperties.DbSchema
        );

        optionsAction?.Invoke(options);


        builder.Entity<FileDescriptor>(b =>
        {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Files", options.Schema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.ContainerName).IsRequired().HasMaxLength(FileDescriptorConsts.MaxContainerNameLength);
            b.Property(q => q.BlobName).IsRequired().HasMaxLength(FileDescriptorConsts.MaxBlobNameLength);
            b.Property(q => q.EntityTypeFullName).HasMaxLength(FileDescriptorConsts.MaxEntityTypeFullNameLength);
            b.Property(q => q.EntityId).HasMaxLength(FileDescriptorConsts.MaxEntityIdLength);
            b.Property(q => q.Hash).HasMaxLength(FileDescriptorConsts.MaxBlobHashLength);
            b.Property(q => q.ReferBlobName).HasMaxLength(FileDescriptorConsts.MaxBlobNameLength);
            b.Property(q => q.Name).HasMaxLength(FileDescriptorConsts.MaxNameLength);
            b.Property(q => q.MimeType).HasMaxLength(FileDescriptorConsts.MaxMimeTypeLength);



            //Indexes
            b.HasIndex(q => new { q.TenantId, q.EntityTypeFullName, q.EntityId });
            b.HasIndex(q => new { q.TenantId, q.ContainerName, q.BlobName });
            b.HasIndex(q => new { q.TenantId, q.ContainerName, q.ReferBlobName });
            b.HasIndex(q => new { q.TenantId, q.ContainerName, q.Hash });
            b.HasIndex(q => new { q.TenantId, q.CreatorId });
        });

    }
}
