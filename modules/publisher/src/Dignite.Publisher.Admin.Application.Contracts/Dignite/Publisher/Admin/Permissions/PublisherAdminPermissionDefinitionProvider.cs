using Dignite.Publisher.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Dignite.Publisher.Admin.Permissions;

public class PublisherAdminPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var publisherGroup = context.AddGroup(PublisherAdminPermissions.GroupName, L("Permission:Publisher"));

        // Define permissions for the category
        var categoryManagement = publisherGroup.AddPermission(PublisherAdminPermissions.Categories.Default, L("Permission:CategoryManagement"));
        categoryManagement.AddChild(PublisherAdminPermissions.Categories.Create, L("Permission:CategoryManagement:Create"));
        categoryManagement.AddChild(PublisherAdminPermissions.Categories.Update, L("Permission:CategoryManagement:Update"));
        categoryManagement.AddChild(PublisherAdminPermissions.Categories.Delete, L("Permission:CategoryManagement:Delete"));

        // Define permissions for the post
        var postManagement = publisherGroup.AddPermission(PublisherAdminPermissions.Posts.Default, L("Permission:PostManagement"));
        postManagement.AddChild(PublisherAdminPermissions.Posts.Create, L("Permission:PostManagement:Create"));
        postManagement.AddChild(PublisherAdminPermissions.Posts.Update, L("Permission:PostManagement:Update"));
        postManagement.AddChild(PublisherAdminPermissions.Posts.Delete, L("Permission:PostManagement:Delete"));
        postManagement.AddChild(PublisherAdminPermissions.Posts.Publish, L("Permission:PostManagement:Publish"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<PublisherResource>(name);
    }
}
