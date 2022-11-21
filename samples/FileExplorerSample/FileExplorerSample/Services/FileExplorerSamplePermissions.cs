using Volo.Abp.Reflection;

namespace FileExplorerSample.Services;

public class FileExplorerSamplePermissions
{
    public const string GroupName = "FileExplorerSample";

    public static class Files
    {
        public const string Default = GroupName + ".Files";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(FileExplorerSamplePermissions));
    }
}