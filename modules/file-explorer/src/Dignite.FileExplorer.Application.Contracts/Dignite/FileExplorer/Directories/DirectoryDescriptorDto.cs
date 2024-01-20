using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.FileExplorer.Directories;

public class DirectoryDescriptorDto : ExtensibleAuditedEntityDto<Guid>
{
    /// <summary>
    /// Container name of blob
    /// </summary>
    public string ContainerName { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///
    /// </summary>
    public Guid? ParentId { get; set; }
    public int Order { get; set; }
}