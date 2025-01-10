using Volo.Abp.Reflection;

namespace Dignite.Abp.RegionalizationManagement.Permissions;

public class RegionalizationManagementPermissions
{
    public const string GroupName = "RegionalizationManagement";
    public const string ManageRegions = GroupName + ".ManageRegions";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(RegionalizationManagementPermissions));
    }
}
