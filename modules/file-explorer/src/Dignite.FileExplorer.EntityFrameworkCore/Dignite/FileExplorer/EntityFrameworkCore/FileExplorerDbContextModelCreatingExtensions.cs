using Dignite.FileExplorer.Files;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Dignite.FileExplorer.EntityFrameworkCore;

public static class FileExplorerDbContextModelCreatingExtensions
{
    public static void ConfigureFileExplorer(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));


        builder.Entity<FileDescriptor>(b =>
        {
            //Configure table & schema name
            b.ToTable(FileExplorerDbProperties.DbTablePrefix + "FileDescriptors", FileExplorerDbProperties.DbSchema);

            b.ConfigureByConvention();
            
            //Properties
            b.Property(q => q.ContainerName).IsRequired().HasMaxLength(FileDescriptorConsts.MaxContainerNameLength);
            b.Property(q => q.BlobName).IsRequired().HasMaxLength(FileDescriptorConsts.MaxBlobNameLength);
            b.Property(q => q.EntityTypeFullName).HasMaxLength(FileDescriptorConsts.MaxEntityTypeFullNameLength);
            b.Property(q => q.EntityId).HasMaxLength(FileDescriptorConsts.MaxEntityIdLength);
            b.Property(q => q.Name).HasMaxLength(FileDescriptorConsts.MaxNameLength);
            b.Property(q => q.MimeType).HasMaxLength(FileDescriptorConsts.MaxMimeTypeLength);



            //Indexes
            b.HasIndex(q => new { q.TenantId, q.EntityTypeFullName, q.EntityId });
            b.HasIndex(q => new { q.TenantId, q.ContainerName, q.BlobName });
            b.HasIndex(q => new { q.TenantId, q.CreatorId });
        });
        
    }
}
