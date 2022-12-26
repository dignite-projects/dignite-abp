using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dignite.Abp.NotificationCenter.Localization;

namespace Dignite.Abp.NotificationCenter.Blazor.Pages.NotificationCenter;
public partial class NotificationsComponent
{
    /// <summary>
    /// notification total count
    /// </summary>
    private int totalCount = 0;
    private int skipCount = 0;
    private int maxResultCount = 5;

    private List<UserNotificationDto> userNotifications = null;
    private bool Loading=false;

    protected override async Task OnInitializedAsync()
    {
        LocalizationResource = typeof(NotificationCenterResource);
        totalCount = await NotificationAppService.GetCountAsync(Notifications.UserNotificationState.Unread);

        await base.OnInitializedAsync();
    }

    /// <summary>
    /// Load the latest notifications, which are newly sent;
    /// Typically, called by an external event;
    /// </summary>
    /// <returns></returns>
    public async Task LoadNewNotificationsAsync()
    {
        Loading = true;
        await InvokeAsync(StateHasChanged);
        if (userNotifications == null || !userNotifications.Any())
        {
            userNotifications = await GetListAsync();
            Loading = false;
            await InvokeAsync(StateHasChanged);
        }
        else
        {
            var newNotifications = (
                await NotificationAppService.GetListAsync(
                    null,
                    0,
                    100,
                    userNotifications.First().CreationTime,
                    null
                )).Items.ToList();

            userNotifications.InsertRange(0,newNotifications);
            Loading = false;
            await InvokeAsync(StateHasChanged);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private async Task LoadMoreAsync()
    {
        Loading = true;
        userNotifications.AddRange(await GetListAsync());
        Loading = false;
    }

    async Task<List<UserNotificationDto>> GetListAsync()
    {
        var list = (await NotificationAppService.GetListAsync(null, skipCount, maxResultCount))
            .Items
            .ToList();
        if (list.Any())
        {
            skipCount += maxResultCount;
        }
        return list;
    }

    string FormatNotificationTime(DateTime notificationTime)
    {
        TimeSpan nowTimeSpan = new TimeSpan(Clock.Now.Ticks);
        TimeSpan notificationTimeSpan = new TimeSpan(notificationTime.Ticks);
        TimeSpan ts = nowTimeSpan.Subtract(notificationTimeSpan).Duration();
        if (ts.TotalSeconds < 60)
        {
            return L["{0}SecondsAgo", (int)ts.TotalSeconds];
        }
        else if (ts.TotalMinutes < 60)
        {
            return L["{0}MinutesAgo", (int)ts.TotalMinutes];
        }
        else if (ts.TotalHours <24)
        {
            return L["{0}HoursAgo", (int)ts.TotalHours];
        }
        else
        {
            return String.Format("{0:d}", notificationTime);
        }
    }
}
