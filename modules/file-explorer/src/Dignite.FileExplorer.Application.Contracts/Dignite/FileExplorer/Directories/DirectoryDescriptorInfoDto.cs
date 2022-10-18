using System;
using System.Collections.Generic;

namespace Dignite.FileExplorer.Directories;
public class DirectoryDescriptorInfoDto
{
    public Guid Id { get; set; }

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

    public bool HasChildren { get; set; }

    public IList<DirectoryDescriptorInfoDto> Children { get; set; }
}
