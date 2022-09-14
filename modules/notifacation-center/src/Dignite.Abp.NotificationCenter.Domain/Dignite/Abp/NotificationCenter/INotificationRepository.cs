using System;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Abp.NotificationCenter;

public interface INotificationRepository : IBasicRepository<Notification, Guid>
{
}