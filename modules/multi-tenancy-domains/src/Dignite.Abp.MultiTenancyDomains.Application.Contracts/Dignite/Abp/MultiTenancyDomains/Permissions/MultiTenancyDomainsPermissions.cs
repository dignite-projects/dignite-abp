using Volo.Abp.Reflection;

namespace Dignite.Abp.MultiTenancyDomains.Permissions;

public class MultiTenancyDomainsPermissions
{
    public const string GroupName = "MultiTenancyDomains";

    public const string Update = GroupName + ".Update";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(MultiTenancyDomainsPermissions));
    }
}
