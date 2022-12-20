using System.Collections.Generic;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Notifications.Components;

/// <summary>
/// 
/// </summary>
public interface INotificationComponentProvider: ITransientDependency
{
    /// <summary>
    /// 
    /// </summary>
    string NotificationName { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    string GetIcon(string entityId = null);
}
