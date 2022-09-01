using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Notifications
{
    public abstract class NotificationDefinitionProvider : INotificationDefinitionProvider, ITransientDependency
    {
        public abstract void Define(INotificationDefinitionContext context);
    }
}