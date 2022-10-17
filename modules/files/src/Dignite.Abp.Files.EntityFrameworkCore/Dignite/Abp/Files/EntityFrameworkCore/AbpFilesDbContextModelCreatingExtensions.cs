using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dignite.Abp.Files.EntityFrameworkCore;

public static class AbpFilesDbContextModelCreatingExtensions
{
    public static void ConfigureAbpFiles<TFile>(this EntityTypeBuilder<TFile> b)
        where TFile : class, IFile
    {
        b.Property(u => u.ContainerName).IsRequired().HasMaxLength(AbpFileConsts.MaxContainerNameLength).HasColumnName(nameof(IFile.ContainerName));
        b.Property(u => u.BlobName).IsRequired().HasMaxLength(AbpFileConsts.MaxBlobNameLength).HasColumnName(nameof(IFile.BlobName));
        b.Property(u => u.Name).HasMaxLength(AbpFileConsts.MaxNameLength).HasColumnName(nameof(IFile.Name));
        b.Property(u => u.MimeType).HasMaxLength(AbpFileConsts.MaxMimeTypeLength).HasColumnName(nameof(IFile.MimeType));
    }
}