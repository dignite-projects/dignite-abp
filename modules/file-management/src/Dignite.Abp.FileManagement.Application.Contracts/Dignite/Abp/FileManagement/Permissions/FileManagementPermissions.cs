using Volo.Abp.Reflection;

namespace Dignite.Abp.FileManagement.Permissions;

public class FileManagementPermissions
{
    public const string GroupName = "FileManagement";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(FileManagementPermissions));
    }
}
