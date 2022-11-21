using System;
using Volo.Abp.EventBus;

namespace Dignite.FileExplorer.Files;

[EventName("Dignite.FileExplorer.Files.FileDescriptor")]
[Serializable]
public class FileDescriptorEto
{
    public Guid Id { get; set; }

    public string ContainerName { get; set; }

    public string BlobName { get; set; }

    /// <summary>
    /// Directory in container
    /// </summary>
    public Guid? DirectoryId { get; set; }

    public long Size { get; set; }

    public string EntityId { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string Name { get; set; }

    public string MimeType { get; set; }

    public Guid? TenantId { get; set; }
}