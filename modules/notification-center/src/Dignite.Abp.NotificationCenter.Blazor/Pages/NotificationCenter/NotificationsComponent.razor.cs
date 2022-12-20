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
    private int maxResultCount = 20;

    private List<UserNotificationDto> userNotifications = null;



    protected override async Task OnInitializedAsync()
    {
        LocalizationResource = typeof(NotificationCenterResource);
        totalCount = await NotificationAppService.GetCountAsync(null);
        userNotifications = await GetListAsync();

        await base.OnInitializedAsync();
    }


    private async Task LoadMoreAsync()
    {
        userNotifications.AddRange(await GetListAsync());
    }

    async Task<List<UserNotificationDto>> GetListAsync()
    {
        var list= (await NotificationAppService.GetListAsync(Notifications.UserNotificationState.Unread, skipCount, maxResultCount))
            .Items
            .ToList();
        skipCount += maxResultCount;

        return list;
    }
}
