using System;
using Dignite.Abp.BlobStoring;
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
    string ContainerName { get; }

    /// <summary>
    /// Blob name
    /// </summary>
    string BlobName { get; }

    /// <summary>
    /// Blob binary size
    /// </summary>
    long Size { get; }

    /// <summary>
    /// File name
    /// </summary>
    string Name { get; }

    /// <summary>
    /// File mime type
    /// </summary>
    string MimeType { get; }

    /// <summary>
    /// Reset the file size <see cref="IBlobHandler.ExecuteAsync(BlobHandlerContext)"/>
    /// </summary>
    /// <param name="size"></param>
    void Resize(long size);
}