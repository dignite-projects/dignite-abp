using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.FileExplorer.Files;

[Serializable]
public class FileDescriptorDto : CreationAuditedEntityDto<Guid>
{

    public string EntityId { get; set; }

    public string ContainerName { get; set; }

    public string BlobName { get; set; }

    public string CellName { get; set; }

    /// <summary>
    /// Directory in container
    /// </summary>
    public Guid? DirectoryId { get; set; }

    public long Size { get; set; }

    public string Name { get; set; }

    public string MimeType { get; set; }

    public string Url { get; set; }
}