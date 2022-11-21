using Dignite.FileExplorer.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace FileExplorerSample.Services;

public class FileExplorerPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(FileExplorerSamplePermissions.GroupName, L("Permission:FileExplorerSample"));
        var filePermission = myGroup.AddPermission(FileExplorerSamplePermissions.Files.Default, L("Permission:Files"));
        filePermission.AddChild(FileExplorerSamplePermissions.Files.Create, L("Permission:Create"));
        filePermission.AddChild(FileExplorerSamplePermissions.Files.Update, L("Permission:Edit"));
        filePermission.AddChild(FileExplorerSamplePermissions.Files.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<FileExplorerResource>(name);
    }
}