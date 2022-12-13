using System;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Notifications.Components;

public abstract class NotificationDataComponentBase<TNotificationData> : AbpComponentBase,INotificationDataComponent, ITransientDependency
    where TNotificationData:NotificationData
{
    protected NotificationDataComponentBase()
    {
        NotificationDataType = typeof(TNotificationData);
    }


    public Type NotificationDataType { get; private set; }


    [Parameter]
    public TNotificationData NotificationData { get; set; }
}

