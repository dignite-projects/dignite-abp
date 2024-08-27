using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dignite.Abp.NotificationCenter.Blazor.Pages.NotificationCenter;
using Dignite.Abp.Notifications;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

namespace Dignite.Abp.NotificationCenter.Blazor.WebAssembly.Pages.NotificationCenter;
public partial class NotificationsTool : IAsyncDisposable
{
    [Inject]
    public IAccessTokenProvider AccessTokenProvider { get; set; } = default!;

    [Inject]
    public IConfiguration Configuration { get; set; } = default!;

    [Inject] INotificationAppService NotificationAppService { get; set; }

    /// <summary>
    /// Hub Connection
    /// </summary>
    private HubConnection hubConnection;

    /// <summary>
    /// notification count
    /// </summary>
    private int notificationCount = 0;
    private bool hasNewNotifications = true;

    private SubscribeModal SubscribeModalRef;
    private NotificationsComponent NotificationsComponentRef;

    protected override async Task OnInitializedAsync()
    {
        var baseUrl = Configuration.GetValue<string>("RemoteServices:NotificationCenter:BaseUrl");

        if (baseUrl.IsNullOrEmpty())
        {
            baseUrl = Configuration.GetValue<string>("RemoteServices:Default:BaseUrl");
        }

        if (CurrentUser.IsAuthenticated)
        {
            (await AccessTokenProvider.RequestAccessToken()).TryGetToken(out var accessToken);

            hubConnection = new HubConnectionBuilder()
                .WithUrl(baseUrl.EnsureEndsWith('/') + "signalr-hubs/notifications", options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult((string?)accessToken!.Value);
                })
                .Build();

            notificationCount = await NotificationAppService.GetCountAsync(UserNotificationState.Unread);

            hubConnection.On<RealTimeNotifyEto>("ReceiveNotifications", async (eto) =>
            {
                notificationCount++;
                hasNewNotifications = true;

                await InvokeAsync(StateHasChanged);
            });


            await hubConnection.StartAsync();
        }

        await base.OnInitializedAsync();
    }

    private async Task OpenSubscribeModalAsync()
    {
        await SubscribeModalRef.OpenCreateModalAsync();
    }

    public async ValueTask DisposeAsync()
    {
        if (hubConnection is not null)
        {
            await hubConnection.DisposeAsync();
        }
    }

    private async Task OnVisibleChanged(bool isVisible)
    {
        if (isVisible && hasNewNotifications)
        {
            await NotificationsComponentRef.LoadNewNotificationsAsync();
            hasNewNotifications = false;
            await InvokeAsync(StateHasChanged);
        }
    }

    private async Task SetNotificationRead()
    {
        notificationCount--;
        await InvokeAsync(StateHasChanged);
    }
}
