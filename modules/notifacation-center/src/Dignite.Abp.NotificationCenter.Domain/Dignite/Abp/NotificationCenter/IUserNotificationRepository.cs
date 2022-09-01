using Dignite.Abp.Notifications;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Abp.NotificationCenter
{
    public interface IUserNotificationRepository : IBasicRepository<UserNotification>
    {
        Task<UserNotification> FindAsync(Guid userId,Guid notificationId, CancellationToken cancellationToken = default);

        Task<List<UserNotification>> GetListAsync(Guid userId, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default);

        Task<int> GetCountAsync(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default);


        Task<bool> AnyAsync(Guid notificationId, Guid ignoredUserId, CancellationToken cancellationToken = default);
    }
}
