using System;
using System.ComponentModel.DataAnnotations;
using Dignite.Abp.Files;
using JetBrains.Annotations;
using Volo.Abp.Content;
using Volo.Abp.Validation;

namespace Dignite.FileExplorer.Files;

public class CreateFileInput
{
    public CreateFileInput()
    {
        DirectoryId = null;
    }

    /// <summary>
    /// Container name of blob
    /// </summary>
    [Required]
    [DynamicStringLength(typeof(AbpFileConsts), nameof(AbpFileConsts.MaxContainerNameLength))]
    public string ContainerName { get; set; }

    [CanBeNull]
    [DynamicStringLength(typeof(FileDescriptorConsts), nameof(FileDescriptorConsts.MaxCellNameLength))]
    public string? CellName { get; set; }

    /// <summary>
    /// Directory in container
    /// </summary>
    public Guid? DirectoryId { get; set; }

    /// <summary>
    /// Associated Entity Id
    /// </summary>
    [CanBeNull]
    [DynamicStringLength(typeof(FileDescriptorConsts), nameof(FileDescriptorConsts.MaxEntityIdLength))]
    public string? EntityId { get; set; }

    [Required]
    public IRemoteStreamContent File { get; set; }
}