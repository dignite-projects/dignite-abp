using Dignite.Abp.Notifications;

namespace Dignite.Abp.NotificationCenter
{
    public class TestNotificationProvider : NotificationDefinitionProvider
    {
        public override void Define(INotificationDefinitionContext context)
        {
            context.Add(new NotificationDefinition("DefinitionANotification"));
        }
    }
}
