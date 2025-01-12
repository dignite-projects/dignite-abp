using System;
using Dignite.Abp.Files;
using JetBrains.Annotations;
using Volo.Abp.Validation;

namespace Dignite.FileExplorer.Files;

public class UpdateFileInput
{

    [CanBeNull]
    [DynamicStringLength(typeof(FileDescriptorConsts), nameof(FileDescriptorConsts.MaxCellNameLength))]
    public string? CellName { get; set; }

    /// <summary>
    /// Modify the directory of the file
    /// </summary>
    public Guid? DirectoryId { get; set; }

    /// <summary>
    /// Modify the name of the file
    /// </summary>
    [CanBeNull]
    [DynamicStringLength(typeof(FileConsts), nameof(FileConsts.MaxNameLength))]
    public string? Name { get; set; }
}