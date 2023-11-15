using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Dignite.CmsKit.Localization;

namespace Dignite.CmsKit.Permissions;

public class CmsKitPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(CmsKitPermissions.GroupName, L("Permission:CmsKit"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<DigniteCmsKitResource>(name);
    }
}
