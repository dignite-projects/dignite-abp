using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Dignite.FileExplorer.Files;

public class FileDescriptorDto : ExtensibleCreationAuditedEntityDto<Guid>, IMultiTenant
{
    public Guid? TenantId { get; set; }
    public string EntityType { get; set; }

    public string EntityId { get; set; }

    public string ContainerName { get; set; }

    public string BlobName { get; set; }

    /// <summary>
    /// Directory in container
    /// </summary>
    public Guid? DirectoryId { get; set; }

    public long Size { get; set; }

    public string Name { get; set; }

    public string MimeType { get; set; }
}