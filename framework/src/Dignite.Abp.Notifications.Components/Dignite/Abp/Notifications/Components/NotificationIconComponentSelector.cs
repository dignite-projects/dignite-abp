using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Notifications.Components;

public class NotificationIconComponentSelector : INotificationIconComponentSelector, ITransientDependency
{
    private readonly IEnumerable<INotificationIconComponent> _notificationIconComponent;
    public NotificationIconComponentSelector(IEnumerable<INotificationIconComponent> notificationIconComponent)
    {
        _notificationIconComponent = notificationIconComponent;
    }

    /// <summary>
    /// Get Notification Icon blazor component using notificationDefinitionName
    /// </summary>
    /// <param name="notificationDefinitionName">
    /// </param>
    /// <returns></returns>
    [NotNull]
    public INotificationIconComponent GetOrNull(string notificationDefinitionName)
    {
        var notificationDataComponent = _notificationIconComponent.FirstOrDefault(ndc => ndc.NotificationName == notificationDefinitionName);

        return notificationDataComponent;
    }
}

