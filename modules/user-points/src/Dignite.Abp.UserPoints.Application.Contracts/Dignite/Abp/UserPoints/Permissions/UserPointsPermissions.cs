using Volo.Abp.Reflection;

namespace Dignite.Abp.UserPoints.Permissions;

public class UserPointsPermissions
{
    public const string GroupName = "UserPoints";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(UserPointsPermissions));
    }
}
