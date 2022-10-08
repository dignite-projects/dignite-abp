using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Dignite.Abp.NotificationCenter.Blazor.Server.Bundling;

public class NotificationCenterStyleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/_content/Dignite.Abp.NotificationCenter.Blazor/libs/notification-center/css/notification.css");
    }
}