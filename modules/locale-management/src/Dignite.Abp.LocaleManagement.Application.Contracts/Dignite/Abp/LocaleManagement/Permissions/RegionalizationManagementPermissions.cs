using Volo.Abp.Reflection;

namespace Dignite.Abp.LocaleManagement.Permissions;

public class LocaleManagementPermissions
{
    public const string GroupName = "LocaleManagement";
    public const string ManageRegions = GroupName + ".ManageRegions";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(LocaleManagementPermissions));
    }
}
