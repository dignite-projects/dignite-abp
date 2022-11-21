using Dignite.FileExplorer;
using FileExplorerSample.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Volo.Abp.Authorization.Permissions;

namespace FileExplorerSample.Services;

public class SampleEntityAuthorizationHandler: AuthorizationHandler<OperationAuthorizationRequirement, SampleEntity>
{
    private readonly IPermissionChecker _permissionChecker;

    public SampleEntityAuthorizationHandler(IPermissionChecker permissionChecker)
    {
        _permissionChecker = permissionChecker;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement,
        SampleEntity resource)
    {
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

        if (requirement.Name == CommonOperations.Get.Name && await HasGetPermission(context, resource))
        {
            context.Succeed(requirement);
            return;
        }
    }

    private async Task<bool> HasDeletePermission(AuthorizationHandlerContext context, SampleEntity resource)
    {
        return true;
    }

    private async Task<bool> HasUpdatePermission(AuthorizationHandlerContext context, SampleEntity resource)
    {
        return true;
    }

    private async Task<bool> HasCreatePermission(AuthorizationHandlerContext context, SampleEntity resource)
    {
        return true;
    }

    private async Task<bool> HasGetPermission(AuthorizationHandlerContext context, SampleEntity resource)
    {
        return true;
    }
}
