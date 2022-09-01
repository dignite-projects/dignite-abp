using Volo.Abp.Reflection;

namespace Dignite.FileExplorer.Permissions;

public class FileExplorerPermissions
{
    public const string GroupName = "FileExplorer";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(FileExplorerPermissions));
    }
}
