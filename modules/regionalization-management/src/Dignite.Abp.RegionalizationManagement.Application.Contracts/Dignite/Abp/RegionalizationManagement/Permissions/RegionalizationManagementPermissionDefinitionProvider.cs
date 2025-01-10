using Dignite.Abp.RegionalizationManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Dignite.Abp.RegionalizationManagement.Permissions;

public class RegionalizationManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var regionalizationManagementGroup = context.AddGroup(RegionalizationManagementPermissions.GroupName, L("Permission:RegionalizationManagement"));

        regionalizationManagementGroup.AddPermission(
            RegionalizationManagementPermissions.ManageRegions,
            L("Permission:RegionalizationManagement.ManageRegions"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<RegionalizationManagementResource>(name);
    }
}
