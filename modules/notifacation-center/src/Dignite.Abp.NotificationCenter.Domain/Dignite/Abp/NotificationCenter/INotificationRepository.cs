using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Abp.NotificationCenter
{
    public interface INotificationRepository : IBasicRepository<Notification,Guid>
    {
    }
}
