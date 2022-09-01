using System.Threading.Tasks;

namespace Dignite.Abp.AspNetCore.SignalR.Notifications
{
    public interface INotificationClient
    {
        Task ReceiveNotifications();
    }
}
