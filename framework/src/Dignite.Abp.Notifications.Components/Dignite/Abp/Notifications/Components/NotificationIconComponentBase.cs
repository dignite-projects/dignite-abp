using Volo.Abp.AspNetCore.Components;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Notifications.Components;

public abstract class NotificationIconComponentBase : AbpComponentBase,INotificationIconComponent, ITransientDependency
{
    /// <summary>
    /// 
    /// </summary>
    public abstract string NotificationName { get; }
}

