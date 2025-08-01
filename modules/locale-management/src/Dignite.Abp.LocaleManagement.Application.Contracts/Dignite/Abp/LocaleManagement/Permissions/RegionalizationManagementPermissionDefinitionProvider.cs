using Dignite.Abp.LocaleManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Dignite.Abp.LocaleManagement.Permissions;

public class LocaleManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var localeManagementGroup = context.AddGroup(LocaleManagementPermissions.GroupName, L("Permission:LocaleManagement"));

        localeManagementGroup.AddPermission(
            LocaleManagementPermissions.ManageRegions,
            L("Permission:LocaleManagement.ManageRegions"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<LocaleManagementResource>(name);
    }
}
