using System;
using System.ComponentModel.DataAnnotations;

namespace Dignite.FileExplorer.Directories;
public class MoveDirectoryInput
{
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Guid? NewParentId { get; set; }

    [Required]
    public int Order { get; set; }
}
