using Dignite.Abp.UserPoints.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Dignite.Abp.UserPoints.Permissions;

public class UserPointsPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(UserPointsPermissions.GroupName, L("Permission:UserPoints"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<UserPointsResource>(name);
    }
}
