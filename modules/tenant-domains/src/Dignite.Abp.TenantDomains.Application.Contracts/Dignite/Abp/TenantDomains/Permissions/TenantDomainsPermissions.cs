using Volo.Abp.Reflection;

namespace Dignite.Abp.TenantDomains.Permissions;

public class TenantDomainsPermissions
{
    public const string GroupName = "TenantDomains";

    public const string Update = GroupName + ".Update";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(TenantDomainsPermissions));
    }
}
