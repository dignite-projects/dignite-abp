using Dignite.Abp.MultiTenancyDomains.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Dignite.Abp.MultiTenancyDomains.Permissions;

public class MultiTenancyDomainsPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var tenantDomainsGroup = context.AddGroup(MultiTenancyDomainsPermissions.GroupName, L("Permission:MultiTenancyDomains"));

        tenantDomainsGroup.AddPermission(MultiTenancyDomainsPermissions.Update, L("Permission:Update"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MultiTenancyDomainsResource>(name);
    }
}
