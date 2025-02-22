using Volo.Abp.Reflection;

namespace Dignite.Abp.TenantDomainManagement.Permissions;

public class TenantDomainManagementPermissions
{
    public const string GroupName = "TenantDomainManagement";


    public const string ManageDomain = GroupName + ".ManageDomain";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(TenantDomainManagementPermissions));
    }
}
