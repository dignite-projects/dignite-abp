using Dignite.FileExplorer.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Dignite.FileExplorer.Permissions;

public class FileExplorerPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(FileExplorerPermissions.GroupName, L("Permission:FileExplorer"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<FileExplorerResource>(name);
    }
}
