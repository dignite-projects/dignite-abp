using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Notifications.Components;

public class NotificationComponentProviderSelector : INotificationComponentProviderSelector, ITransientDependency
{
    protected IEnumerable<INotificationComponentProvider> NotificationComponentProviders;
    public NotificationComponentProviderSelector(IEnumerable<INotificationComponentProvider> notificationComponentProviders)
    {
        NotificationComponentProviders = notificationComponentProviders;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="notificationName">
    /// </param>
    /// <returns></returns>
    [NotNull]
    public INotificationComponentProvider Get(string notificationName)
    {
        var provider = NotificationComponentProviders.FirstOrDefault(ndc => ndc.NotificationName == notificationName);

        if (provider == null)
            throw new AbpException(
                $"Could not find the notification component provider with the notification name ({notificationName}) ."
            );
        else
            return provider;
    }
}

