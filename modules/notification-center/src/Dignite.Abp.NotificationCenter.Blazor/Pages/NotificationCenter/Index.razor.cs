using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Dignite.Abp.NotificationCenter.Localization;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;

namespace Dignite.Abp.NotificationCenter.Blazor.Pages.NotificationCenter;
public partial class Index
{
    /// <summary>
    /// notification total count
    /// </summary>
    private int totalCount = 0;
    private int skipCount = 0;
    private int maxResultCount = 20;

    private List<UserNotificationDto> userNotifications = null;
    private IReadOnlyList<NotificationSubscriptionDto> availableSubscriptions=new List<NotificationSubscriptionDto>();

    private PageToolbar Toolbar { get; } = new();
    private Modal CreateModal;


    protected override async Task OnInitializedAsync()
    {
        LocalizationResource = typeof(NotificationCenterResource);
        totalCount = await NotificationAppService.GetCountAsync(null);
        userNotifications = await GetListAsync();
        availableSubscriptions = (await NotificationAppService.GetAllAvailableSubscribeAsync()).Items;


        //Set each notification as read
        await NotificationAppService.UpdateAllStatesAsync(Notifications.UserNotificationState.Read);

        await base.OnInitializedAsync();
    }

    private async Task SetToolbarItemsAsync()
    {
        Toolbar.AddButton(
            "", 
            OpenCreateModalAsync,
            "fa fa-cog");

        await InvokeAsync(StateHasChanged);
    }

    protected async override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await SetToolbarItemsAsync();
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task LoadMoreAsync()
    {
        userNotifications.AddRange(await GetListAsync());
    }

    async Task<List<UserNotificationDto>> GetListAsync()
    {
        var list= (await NotificationAppService.GetListAsync(null, skipCount, maxResultCount))
            .Items
            .ToList();
        skipCount += maxResultCount;

        return list;
    }

    private async Task OpenCreateModalAsync()
    {
        try
        {
            await InvokeAsync(async () =>
            {
                StateHasChanged();
                if (CreateModal != null)
                {
                    await CreateModal.Show();
                }

            });
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    private Task ClosingCreateModal(ModalClosingEventArgs eventArgs)
    {
        // cancel close if clicked outside of modal area
        eventArgs.Cancel = eventArgs.CloseReason == CloseReason.FocusLostClosing;

        return Task.CompletedTask;
    }

    private Task CloseCreateModalAsync()
    {
        return InvokeAsync(CreateModal.Hide);
    }

    private async Task OnSubscribeChanged(bool isSubscribe,string notificationName)
    {
        try
        {
            availableSubscriptions.Single(s => s.NotificationName == notificationName).IsSubscribed=isSubscribe;

            if (isSubscribe)
                await NotificationAppService.SubscribeAsync(notificationName);
            else
                await NotificationAppService.UnsubscribeAsync(notificationName);

        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }
}
