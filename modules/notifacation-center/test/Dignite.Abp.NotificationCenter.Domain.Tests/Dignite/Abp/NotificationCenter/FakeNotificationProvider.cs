using Dignite.Abp.Notifications;

namespace Dignite.Abp.NotificationCenter;

public class FakeNotificationProvider : NotificationDefinitionProvider
{
    public override void Define(INotificationDefinitionContext context)
    {
        context.Add(new NotificationDefinition("fake.notification"));
    }
}