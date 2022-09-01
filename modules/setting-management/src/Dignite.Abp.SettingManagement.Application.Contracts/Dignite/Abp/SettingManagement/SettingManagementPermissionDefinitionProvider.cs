using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;
using Volo.Abp.SettingManagement.Localization;

namespace Dignite.Abp.SettingManagement
{
    public class SettingManagementPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var moduleGroup = context.AddGroup(SettingManagementPermissions.GroupName, L("Permission:SettingManagement")); //L("Settings")：The multi language of Volo.ABP.Settingmanagement.Domain.Shared module is used
            moduleGroup.AddPermission(SettingManagementPermissions.Global, L("Permission:GlobalSettings"), multiTenancySide: MultiTenancySides.Host);    //Whether it is host or tenant, the permission name is L("Settings")
            moduleGroup.AddPermission(SettingManagementPermissions.Tenant, L("Permission:TenantSettings"), multiTenancySide: MultiTenancySides.Tenant);
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpSettingManagementResource>(name);
        }
    }
}
