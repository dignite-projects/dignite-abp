using Volo.Abp.Reflection;

namespace Dignite.CmsKit.Permissions;

public class CmsKitPermissions
{
    public const string GroupName = "DigniteCmsKit";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(CmsKitPermissions));
    }
}
