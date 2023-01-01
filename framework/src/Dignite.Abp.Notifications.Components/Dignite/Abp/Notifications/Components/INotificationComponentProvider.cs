using System;
using System.Threading.Tasks;
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
    Type IconComponentType { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Task NotificationClickAsync(NotificationClickArgs args);
}
