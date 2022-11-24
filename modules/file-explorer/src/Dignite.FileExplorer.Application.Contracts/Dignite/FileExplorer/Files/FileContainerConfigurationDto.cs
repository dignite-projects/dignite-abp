namespace Dignite.FileExplorer.Files;
public class FileContainerConfigurationDto
{
    /// <summary>
    /// Limit file size(KB)
    /// </summary>
    public int MaxBlobSize { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string[] AllowedFileTypeNames { get; set; }


    /// <summary>
    /// The permission name of the create directories;
    /// When permissions are not set, no user can create a directory;
    /// </summary>
    public string CreateDirectoryPermissionName { get; set; }

    /// <summary>
    /// The permission name of the create files;
    /// When permissions are not set, no user can create a file;
    /// </summary>
    public string CreateFilePermissionName { get; set; }

    /// <summary>
    /// The permission name of the update files;
    /// When permissions are not set, only the file owner can update
    /// </summary>
    public string UpdateFilePermissionName { get; set; }

    /// <summary>
    /// The permission name of the delete files;
    /// When permissions are not set, only the file owner can delete
    /// </summary>
    public string DeleteFilePermissionName { get; set; }

    /// <summary>
    /// The permission name of the get files;
    /// When permissions are not set, all users will be authorized to get files;
    /// </summary>
    public string GetFilePermissionName { get; set; }
}
