using JetBrains.Annotations;

namespace Dignite.Abp.Notifications.Components;

/// <summary>
/// Notification Data Component Selector
/// </summary>
public interface INotificationIconComponentSelector
{
    /// <summary>
    /// Get Notification Icon blazor component using notificationDefinitionName
    /// </summary>
    /// <param name="notificationDefinitionName">
    /// </param>
    /// <returns></returns>
    [NotNull]
    INotificationIconComponent GetOrNull(string notificationDefinitionName);
}

