using Dignite.Abp.Notifications;
using NotificationCenterSample.Localization;
using Volo.Abp.Localization;

namespace NotificationCenterSample.Notifications;

public class NotificationCenterSampleNotificationDefinitionProvider : NotificationDefinitionProvider
{
    public override void Define(INotificationDefinitionContext context)
    {
        context.Add(new NotificationDefinition("TestNotification",null,L("TestNotification"), L("TestNotificationDescription")));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<NotificationCenterSampleResource>(name);
    }
}
