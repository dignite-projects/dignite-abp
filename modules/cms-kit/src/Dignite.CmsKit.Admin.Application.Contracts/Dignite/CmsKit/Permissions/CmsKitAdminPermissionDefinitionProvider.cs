using Dignite.CmsKit.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Dignite.CmsKit.Permissions;

public class CmsKitAdminPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var cmsGroup = context.GetGroupOrNull(CmsKitAdminPermissions.GroupName) ?? context.AddGroup(CmsKitAdminPermissions.GroupName, L("Permission:CmsKit"));

        var brandGroup = cmsGroup.AddPermission(CmsKitAdminPermissions.Brand.Default, L("Permission:Brand"));
        brandGroup.AddChild(CmsKitAdminPermissions.Brand.Update, L("Permission:Brand.Update"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<DigniteCmsKitResource>(name);
    }
}
