using Dignite.Abp.NotificationCenter.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Dignite.Abp.NotificationCenter.Permissions
{
    public class NotificationCenterPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(NotificationCenterPermissions.GroupName, L("Permission:NotificationCenter"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<NotificationCenterResource>(name);
        }
    }
}