using System;
using System.Security.Principal;
using System.Threading.Tasks;
using Dignite.Abp.BlobStoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.BlobStoring;

namespace Dignite.FileExplorer.Directories;

public class DirectoryDescriptorAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, DirectoryDescriptor>
{
    protected IBlobContainerConfigurationProvider BlobContainerConfigurationProvider { get; }
    protected IPermissionChecker PermissionChecker { get; }

    public DirectoryDescriptorAuthorizationHandler(
        IPermissionChecker permissionChecker,
        IBlobContainerConfigurationProvider blobContainerConfigurationProvider)
    {
        PermissionChecker = permissionChecker;
        BlobContainerConfigurationProvider = blobContainerConfigurationProvider;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement,
        DirectoryDescriptor resource)
    {
        if (requirement.Name == CommonOperations.Get.Name && await HasGetPermission(context, resource))
        {
            context.Succeed(requirement);
            return;
        }
        if (requirement.Name == CommonOperations.Delete.Name && await HasDeletePermission(context, resource))
        {
            context.Succeed(requirement);
            return;
        }

        if (requirement.Name == CommonOperations.Update.Name && await HasUpdatePermission(context, resource))
        {
            context.Succeed(requirement);
            return;
        }

        if (requirement.Name == CommonOperations.Create.Name && await HasCreatePermission(context, resource))
        {
            context.Succeed(requirement);
            return;
        }
    }

    private async Task<bool> HasGetPermission(AuthorizationHandlerContext context, DirectoryDescriptor resource)
    {
        if (resource.CreatorId != null && resource.CreatorId == context.User.FindUserId())
        {
            return await Task.FromResult(true);
        }

        return false;
    }

    private async Task<bool> HasDeletePermission(AuthorizationHandlerContext context, DirectoryDescriptor resource)
    {
        if (resource.CreatorId != null && resource.CreatorId == context.User.FindUserId())
        {
            return await Task.FromResult(true);
        }

        return false;
    }

    private async Task<bool> HasUpdatePermission(AuthorizationHandlerContext context, DirectoryDescriptor resource)
    {
        if (resource.CreatorId != null && resource.CreatorId == context.User.FindUserId())
        {
            return await Task.FromResult(true);
        }

        return false;
    }

    private async Task<bool> HasCreatePermission(AuthorizationHandlerContext context, DirectoryDescriptor resource)
    {
        var containerConfiguration = BlobContainerConfigurationProvider.Get(resource.ContainerName);
        var authorizationConfiguration = containerConfiguration.GetAuthorizationConfiguration();
        if (authorizationConfiguration.CreateDirectoryPermissionName.IsNullOrEmpty())
        {
            return false;
        }
        if (await PermissionChecker.IsGrantedAsync(context.User, authorizationConfiguration.CreateDirectoryPermissionName))
        {
            return true;
        }

        return false;
    }
}