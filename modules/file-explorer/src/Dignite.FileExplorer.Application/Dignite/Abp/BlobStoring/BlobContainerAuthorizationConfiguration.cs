using System;
using Volo.Abp.BlobStoring;

namespace Dignite.Abp.BlobStoring;

public class BlobContainerAuthorizationConfiguration
{
    /// <summary>
    /// The permission name of the create directories;
    /// When permissions are not set, no user can create a directory;
    /// </summary>
    public string CreateDirectoryPermissionName {
        get => _containerConfiguration.GetConfigurationOrDefault<string>(BlobContainerAuthorizationConfigurationNames.CreateDirectoryPermissionName);
        set => _containerConfiguration.SetConfiguration(BlobContainerAuthorizationConfigurationNames.CreateDirectoryPermissionName, value);
    }

    /// <summary>
    /// The permission name of the create files;
    /// When permissions are not set, no user can create a file;
    /// </summary>
    public string CreateFilePermissionName {
        get => _containerConfiguration.GetConfigurationOrDefault<string>(BlobContainerAuthorizationConfigurationNames.CreateFilePermission);
        set => _containerConfiguration.SetConfiguration(BlobContainerAuthorizationConfigurationNames.CreateFilePermission, value);
    }

    /// <summary>
    /// The permission name of the update files;
    /// When permissions are not set, only the file owner can update
    /// </summary>
    public string UpdateFilePermissionName {
        get => _containerConfiguration.GetConfigurationOrDefault<string>(BlobContainerAuthorizationConfigurationNames.UpdateFilePermission);
        set => _containerConfiguration.SetConfiguration(BlobContainerAuthorizationConfigurationNames.UpdateFilePermission, value);
    }

    /// <summary>
    /// The permission name of the delete files;
    /// When permissions are not set, only the file owner can delete
    /// </summary>
    public string DeleteFilePermissionName {
        get => _containerConfiguration.GetConfigurationOrDefault<string>(BlobContainerAuthorizationConfigurationNames.DeleteFilePermission);
        set => _containerConfiguration.SetConfiguration(BlobContainerAuthorizationConfigurationNames.DeleteFilePermission, value);
    }

    /// <summary>
    /// The permission name of the get files;
    /// When permissions are not set, all users will be authorized to get files;
    /// </summary>
    public string GetFilePermissionName {
        get => _containerConfiguration.GetConfigurationOrDefault<string>(BlobContainerAuthorizationConfigurationNames.GetFilePermission);
        set => _containerConfiguration.SetConfiguration(BlobContainerAuthorizationConfigurationNames.GetFilePermission, value);
    }

    /// <summary>
    /// File descriptor associated entity authorization check handler type
    /// This type inherits the <see cref="IFileDescriptorEntityAuthorizationHandler"/> interface
    /// </summary>
    public Type FileEntityAuthorizationHandler {
        get => _containerConfiguration.GetConfigurationOrDefault<Type>(BlobContainerAuthorizationConfigurationNames.FileDescriptorEntityAuthorizationHandler);
    }

    private readonly BlobContainerConfiguration _containerConfiguration;

    public BlobContainerAuthorizationConfiguration(BlobContainerConfiguration containerConfiguration)
    {
        _containerConfiguration = containerConfiguration;
    }

    public void SetAuthorizationHandler<TAuthorizationHandler>()
            where TAuthorizationHandler : class, IFileDescriptorEntityAuthorizationHandler
    {
        _containerConfiguration.SetConfiguration(BlobContainerAuthorizationConfigurationNames.FileDescriptorEntityAuthorizationHandler, typeof(TAuthorizationHandler));
    }
}