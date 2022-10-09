using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dignite.Abp.Notifications;
using Microsoft.AspNetCore.SignalR.Client;
using Volo.Abp.Application.Dtos;

namespace Dignite.Abp.NotificationCenter.Blazor.Pages.NotificationCenter;
public partial class Index
{
    /// <summary>
    /// notification total count
    /// </summary>
    private int totalCount = 0;
    private int skipCount = 0;
    private int maxResultCount = 30;

    private List<UserNotificationDto> userNotifications = null;


    protected override async Task OnInitializedAsync()
    {
        totalCount = await NotificationAppService.GetCountAsync(null);
        userNotifications = await GetListAsync();
        await base.OnInitializedAsync();
    }

    private async Task LoadMoreAsync()
    {
        skipCount += maxResultCount;
        userNotifications.AddRange(await GetListAsync());
    }

    async Task<List<UserNotificationDto>> GetListAsync()
    {
        return (await NotificationAppService.GetListAsync(null, skipCount, maxResultCount))
            .Items
            .ToList();
    }
}
