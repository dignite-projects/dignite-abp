using Volo.Abp.Reflection;

namespace Dignite.FileExplorer.Permissions;

public class FileExplorerPermissions
{
    public const string GroupName = "FileExplorer";

    public static class Files
    {
        public const string Default = GroupName + ".File";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
        public const string Management = Default + ".Management";
    }

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(FileExplorerPermissions));
    }
}