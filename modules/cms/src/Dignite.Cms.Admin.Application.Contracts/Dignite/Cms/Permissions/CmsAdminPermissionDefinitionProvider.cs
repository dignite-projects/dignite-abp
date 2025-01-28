using Dignite.Cms.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Dignite.Cms.Permissions
{
    public class CmsAdminPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var group = context.AddGroup(CmsAdminPermissions.GroupName, L("Permission:CmsAdmin"));

            var domains = group.AddPermission(CmsAdminPermissions.Domain.Default, L("Permission:Domain"));
            domains.AddChild(CmsAdminPermissions.Domain.Update, L("Permission:Edit"));

            var fields = group.AddPermission(CmsAdminPermissions.Field.Default, L("Permission:Field"));
            fields.AddChild(CmsAdminPermissions.Field.Create, L("Permission:Create"));
            fields.AddChild(CmsAdminPermissions.Field.Update, L("Permission:Edit"));
            fields.AddChild(CmsAdminPermissions.Field.Delete, L("Permission:Delete"));

            var sections = group.AddPermission(CmsAdminPermissions.Section.Default, L("Permission:Section"));
            sections.AddChild(CmsAdminPermissions.Section.Create, L("Permission:Create"));
            sections.AddChild(CmsAdminPermissions.Section.Update, L("Permission:Edit"));
            sections.AddChild(CmsAdminPermissions.Section.Delete, L("Permission:Delete"));

            var entries = group.AddPermission(CmsAdminPermissions.Entry.Default, L("Permission:Entry"));
            entries.AddChild(CmsAdminPermissions.Entry.Create, L("Permission:Create"));
            entries.AddChild(CmsAdminPermissions.Entry.Update, L("Permission:Edit"));
            entries.AddChild(CmsAdminPermissions.Entry.Delete, L("Permission:Delete"));

            group.AddPermission(CmsAdminPermissions.Settinging, L("Permission:Settinging"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<CmsResource>(name);
        }
    }
}