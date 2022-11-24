using System;

namespace Dignite.FileExplorer.Files;

public class UpdateFileInput
{
    /// <summary>
    /// Modify the directory of the file
    /// </summary>
    public Guid? DirectoryId { get; set; }

    /// <summary>
    /// Modify the name of the file
    /// </summary>
    public string Name { get; set; }
}