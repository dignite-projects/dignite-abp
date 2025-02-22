using Dignite.Abp.TenantDomainManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Dignite.Abp.TenantDomainManagement.Permissions;

public class TenantDomainManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var tenantDomainGroup = context.AddGroup(TenantDomainManagementPermissions.GroupName, L("Permission:TenantDomainManagement"));

        tenantDomainGroup.AddPermission(TenantDomainManagementPermissions.ManageDomain, L("Permission:ManageDomain"), Volo.Abp.MultiTenancy.MultiTenancySides.Tenant);
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TenantDomainManagementResource>(name);
    }
}
