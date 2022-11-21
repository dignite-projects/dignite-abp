using Dignite.Abp.Files;
using Dignite.Abp.Files.EntityFrameworkCore;
using Dignite.FileExplorer.Directories;
using Dignite.FileExplorer.Files;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Dignite.FileExplorer.EntityFrameworkCore;

public static class FileExplorerDbContextModelCreatingExtensions
{
    public static void ConfigureFileExplorer(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<DirectoryDescriptor>(b =>
        {
            //Configure table & schema name
            b.ToTable(FileExplorerDbProperties.DbTablePrefix + "DirectoryDescriptors", FileExplorerDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.ContainerName).IsRequired().HasMaxLength(AbpFileConsts.MaxContainerNameLength);
            b.Property(q => q.Name).IsRequired().HasMaxLength(DirectoryDescriptorConsts.MaxNameLength);

            //Indexes
            b.HasIndex(q => new { q.TenantId, q.ContainerName, q.CreatorId, q.ParentId });

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<FileDescriptor>(b =>
        {
            //Configure table & schema name
            b.ToTable(FileExplorerDbProperties.DbTablePrefix + "FileDescriptors", FileExplorerDbProperties.DbSchema);

            b.ConfigureByConvention();
            b.ConfigureAbpFiles();

            //Properties
            b.Property(q => q.EntityId).HasMaxLength(FileDescriptorConsts.MaxEntityIdLength);

            //Indexes
            b.HasIndex(q => new { q.TenantId, q.EntityId });
            b.HasIndex(q => new { q.TenantId, q.ContainerName, q.BlobName });
            b.HasIndex(q => new { q.TenantId, q.ContainerName, q.CreatorId, q.DirectoryId });
            b.HasIndex(q => new { q.TenantId, q.CreatorId });

            b.ApplyObjectExtensionMappings();
        });
    }
}