using System;
using System.ComponentModel.DataAnnotations;
using Dignite.Abp.Files;
using Volo.Abp.Validation;

namespace Dignite.FileExplorer.Directories;

public class CreateDirectoryInput
{
    /// <summary>
    /// Container name of blob
    /// </summary>
    [Required]
    [DynamicMaxLength(typeof(FileConsts), nameof(FileConsts.MaxContainerNameLength))]
    public string ContainerName { get; set; }

    /// <summary>
    ///
    /// </summary>
    [Required]
    [DynamicMaxLength(typeof(DirectoryDescriptorConsts), nameof(DirectoryDescriptorConsts.MaxNameLength))]
    public string Name { get; set; }

    /// <summary>
    ///
    /// </summary>
    public Guid? ParentId { get; set; }
}