using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dignite.Abp.Files.EntityFrameworkCore;

public static class AbpFilesDbContextModelCreatingExtensions
{
    public static void ConfigureAbpFiles<TFile>(this EntityTypeBuilder<TFile> b)
        where TFile : class, IFile
    {
        b.Property(u => u.ContainerName).IsRequired().HasMaxLength(FileConsts.MaxContainerNameLength).HasColumnName(nameof(IFile.ContainerName));
        b.Property(u => u.BlobName).IsRequired().HasMaxLength(FileConsts.MaxBlobNameLength).HasColumnName(nameof(IFile.BlobName));
        b.Property(u => u.Name).HasMaxLength(FileConsts.MaxNameLength).HasColumnName(nameof(IFile.Name));
        b.Property(u => u.MimeType).HasMaxLength(FileConsts.MaxMimeTypeLength).HasColumnName(nameof(IFile.MimeType));
        b.Property(u => u.Md5).HasMaxLength(FileConsts.MaxMd5Length).HasColumnName(nameof(IFile.Md5));
        b.Property(u => u.ReferBlobName).HasMaxLength(FileConsts.MaxBlobNameLength).HasColumnName(nameof(IFile.ReferBlobName));
    }
}