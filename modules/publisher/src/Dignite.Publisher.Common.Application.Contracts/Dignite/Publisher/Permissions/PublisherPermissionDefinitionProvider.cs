using Dignite.Publisher.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Dignite.Publisher.Permissions;

public class PublisherPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(PublisherPermissions.GroupName, L("Permission:Publisher.Common"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<PublisherResource>(name);
    }
}
