using System;
using System.Net;
using System.Threading.Tasks;
using Dignite.Abp.Notifications;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR.Client;

namespace Dignite.Abp.NotificationCenter.Blazor.Server.Pages.NotificationCenter;
public partial class NotificationsTool: IAsyncDisposable
{
    [Inject]
    private IHttpContextAccessor HttpContextAccessor { get; set; }

    [Inject]
    private NavigationManager Navigation { get; set; }

    [Inject] INotificationAppService NotificationAppService { get; set; }

    /// <summary>
    /// Hub Connection
    /// </summary>
    private HubConnection hubConnection;


    /// <summary>
    /// notification count
    /// </summary>
    private int notificationCount = 0;

    protected override async Task OnInitializedAsync()
    {
        if (CurrentUser.IsAuthenticated)
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(Navigation.ToAbsoluteUri("/signalr-hubs/notifications"), options =>
                {
                    if (HttpContextAccessor.HttpContext != null)
                    {
                        foreach (var cookie in HttpContextAccessor.HttpContext.Request.Cookies)
                        {
                            options.Cookies.Add(new Cookie(cookie.Key, cookie.Value, null, HttpContextAccessor.HttpContext.Request.Host.Host));
                        }
                    }
                })
                .Build();

            notificationCount = await NotificationAppService.GetCountAsync(UserNotificationState.Unread);

            hubConnection.On<RealTimeNotifyEto>("ReceiveNotifications", (notification) =>
            {
                notificationCount++;
                InvokeAsync(StateHasChanged);
            });

            await hubConnection.StartAsync();
        }

        await base.OnInitializedAsync();
    }

    public async ValueTask DisposeAsync()
    {
        if (hubConnection is not null)
        {
            await hubConnection.DisposeAsync();
        }
    }

    private void NavigateToNotificationCenter()
    {
        Navigation.NavigateTo("/notification-center");
    }
}
