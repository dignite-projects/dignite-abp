using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

namespace Dignite.Abp.Files;

public class BasicFile : AggregateRoot<Guid>, IFile
{
    public BasicFile()
    {
    }

    public BasicFile(
        [NotNull] string containerName,
        [NotNull] string blobName
        )
    {
        ContainerName = containerName;
        BlobName = blobName;
    }


    public Guid? TenantId { get; set; }

    [NotNull]
    public string ContainerName { get; private set; }

    [NotNull]
    public string BlobName { get; private set; }

    public long Size { get; set; }


    /// <summary>
    /// File name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// File mime type
    /// </summary>
    public string MimeType { get; set; }
}
