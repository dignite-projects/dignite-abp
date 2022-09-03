using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Dignite.Abp.Files;

/// <summary>
/// Blob info interface
/// </summary>
public interface IFile : IAggregateRoot<Guid>, IMultiTenant
{
    /// <summary>
    /// Container name of blob
    /// </summary>
    [NotNull] string ContainerName { get; }

    /// <summary>
    /// Blob name
    /// </summary>
    [NotNull] string BlobName { get; }

    /// <summary>
    /// Blob binary size
    /// </summary>
    long Size { get; set; }

    /// <summary>
    /// File name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// File mime type
    /// </summary>
    public string MimeType { get; set; }
}
