using System;

namespace Dignite.FileExplorer.Directories;

public class MoveDirectoryInput
{
    public MoveDirectoryInput()
    {
    }

    public MoveDirectoryInput(Guid? parentId, int order)
    {
        ParentId = parentId;
        Order = order;
    }

    /// <summary>
    ///
    /// </summary>
    public Guid? ParentId { get; set; }

    public int Order { get; set; }
}