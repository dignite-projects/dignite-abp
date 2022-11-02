namespace Dignite.Abp.BlobStoring;

public static class BlobContainerAuthorizationConfigurationNames
{
    /// <summary>
    /// Create directory permission name;
    /// </summary>
    public const string CreateDirectoryPermissionName = "Authorization.CreateDirectoryPermissionName";

    /// <summary>
    /// Create file permission name;
    /// </summary>
    public const string CreateFilePermission = "Authorization.CreateFilePermissionName";

    /// <summary>
    /// Update file permission name;
    /// </summary>
    public const string UpdateFilePermission = "Authorization.UpdateFilePermissionName";

    /// <summary>
    /// Delete file permission name;
    /// </summary>
    public const string DeleteFilePermission = "Authorization.DeleteFilePermissionName";

    /// <summary>
    /// Get the permission name of the file
    /// </summary>
    public const string GetFilePermission = "Authorization.GetFilePermissionName";

    /// <summary>
    /// File descriptor associated entity authorization check handler type
    /// </summary>
    public const string FileDescriptorEntityAuthorizationHandler = "Authorization.FileDescriptorEntityAuthorizationHandler";
}