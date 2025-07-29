using Dignite.Publisher.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Dignite.Publisher.Public.Permissions;

public class PublisherPublicPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(PublisherPublicPermissions.GroupName, L("Permission:Publisher.Public"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<PublisherResource>(name);
    }
}
