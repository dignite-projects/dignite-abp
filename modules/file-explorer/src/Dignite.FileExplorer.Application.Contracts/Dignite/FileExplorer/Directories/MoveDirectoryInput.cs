using System;

namespace Dignite.FileExplorer.Directories;

public class MoveDirectoryInput
{
    /// <summary>
    ///
    /// </summary>
    public Guid TargetId { get; set; }

    public DirectoryMovePosition Position { get; set; }
}