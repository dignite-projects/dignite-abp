using Dignite.Abp.TenantDomains.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Dignite.Abp.TenantDomains.Permissions;

public class TenantDomainsPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var tenantDomainsGroup = context.AddGroup(TenantDomainsPermissions.GroupName, L("Permission:TenantDomains"));

        tenantDomainsGroup.AddPermission(TenantDomainsPermissions.Update, L("Permission:Update"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TenantDomainsResource>(name);
    }
}
