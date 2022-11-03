using System;
using Volo.Abp.Domain.Entities;

namespace Dignite.Abp.Files;

/// <summary>
/// Blob info interface
/// </summary>
public interface IFile : IAggregateRoot<Guid>
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
    /// File md5
    /// </summary>
    string Md5 { get; }

    /// <summary>
    /// Referencing other blob
    /// </summary>
    string ReferBlobName { get; }

    /// <summary>
    /// Set the file size
    /// </summary>
    /// <param name="size"></param>
    void SetSize(long size);

    /// <summary>
    /// Set the file md5
    /// </summary>
    /// <param name="md5"></param>
    void SetMd5(string md5);

    /// <summary>
    /// Set the file ReferBlobName
    /// </summary>
    /// <param name="blobName"></param>
    void SetReferBlobName(string blobName);
}