namespace Dignite.CmsKit.Permissions;

public static class CmsKitAdminPermissions
{
    public const string GroupName = "CmsKitAdmin";

    public static class Brand
    {
        public const string Default = GroupName + ".Brand";
        public const string Update = Default + ".Update";
    }
}
