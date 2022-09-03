using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.FileExplorer.Files;

public class FileDescriptorDto : CreationAuditedEntityDto<Guid>
{
    public string EntityTypeFullName { get; set; }

    public string EntityId { get; set; }

    public string ContainerName { get; set; }

    public string BlobName { get; set; }

    public long Size { get; set; }

    public string Name { get; set; }

    public string MimeType { get; set; }
}
