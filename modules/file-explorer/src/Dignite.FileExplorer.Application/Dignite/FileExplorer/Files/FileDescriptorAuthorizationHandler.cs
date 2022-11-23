using System;
using System.Security.Principal;
using System.Threading.Tasks;
using Dignite.Abp.BlobStoring;
using Dignite.FileExplorer.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.BlobStoring;

namespace Dignite.FileExplorer.Files;

public class FileDescriptorAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, FileDescriptor>
{
    protected IServiceProvider ServiceProvider { get; }
    protected IBlobContainerConfigurationProvider BlobContainerConfigurationProvider { get; }
    protected IPermissionChecker PermissionChecker { get; }

    public FileDescriptorAuthorizationHandler(
        IServiceProvider serviceProvider,
        IPermissionChecker permissionChecker,
        IBlobContainerConfigurationProvider blobContainerConfigurationProvider)
    {
        ServiceProvider = serviceProvider;
        PermissionChecker = permissionChecker;
        BlobContainerConfigurationProvider = blobContainerConfigurationProvider;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement,
        FileDescriptor resource)
    {
        var containerConfiguration = BlobContainerConfigurationProvider.Get(resource.ContainerName);
        var authorizationConfiguration = containerConfiguration.GetAuthorizationConfiguration();
        var permissionName = GetPermissionName(authorizationConfiguration, requirement);

        if ((resource.CreatorId.HasValue && resource.CreatorId == context.User.FindUserId())
            || ((!permissionName.IsNullOrEmpty() && await PermissionChecker.IsGrantedAsync(context.User, permissionName))
                || (permissionName.IsNullOrEmpty() && requirement.Name == CommonOperations.Get.Name) // When permissions are not set, all users will be authorized to get files;
                )
            || await PermissionChecker.IsGrantedAsync(context.User, FileExplorerPermissions.Files.Management)
            )
        {
            // File descriptor associated entity authorization check handler
            if (!resource.EntityId.IsNullOrEmpty() && authorizationConfiguration.FileEntityAuthorizationHandler != null)
            {
                using (var scope = ServiceProvider.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetRequiredService(authorizationConfiguration.FileEntityAuthorizationHandler)
                        .As<IFileDescriptorEntityAuthorizationHandler>();

                    await handler.CheckAsync(resource, requirement);
                }
            }
            context.Succeed(requirement);
            return;
        }
    }

    private string GetPermissionName(BlobContainerAuthorizationConfiguration authorizationConfiguration, OperationAuthorizationRequirement requirement)
    {
        if (requirement.Name == CommonOperations.Create.Name)
        {
            return authorizationConfiguration?.CreateFilePermissionName;
        }

        if (requirement.Name == CommonOperations.Delete.Name)
        {
            return authorizationConfiguration?.DeleteFilePermissionName;
        }

        if (requirement.Name == CommonOperations.Update.Name)
        {
            return authorizationConfiguration?.UpdateFilePermissionName;
        }
        if (requirement.Name == CommonOperations.Get.Name)
        {
            return authorizationConfiguration?.GetFilePermissionName;
        }

        return null;
    }
}