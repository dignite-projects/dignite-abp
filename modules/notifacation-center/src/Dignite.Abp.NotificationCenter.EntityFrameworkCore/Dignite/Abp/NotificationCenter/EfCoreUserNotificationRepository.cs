using Dignite.Abp.NotificationCenter.EntityFrameworkCore;
using Dignite.Abp.Notifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Abp.NotificationCenter
{
    public class EfCoreUserNotificationRepository : EfCoreRepository<INotificationCenterDbContext, UserNotification>, IUserNotificationRepository
    {
        public EfCoreUserNotificationRepository(
            IDbContextProvider<INotificationCenterDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<UserNotification> FindAsync(Guid userId, Guid notificationId, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                       .FirstOrDefaultAsync(un => 
                           un.UserId==userId && un.NotificationId==notificationId, 
                           GetCancellationToken(cancellationToken)
                       );
        }

        public async Task<List<UserNotification>> GetListAsync(Guid userId, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Include(un=>un.Notification)
                .WhereIf(state!=null,un=>un.State==state)
                .WhereIf(startDate != null, un => un.Notification.CreationTime>=startDate.Value)
                .WhereIf(endDate != null, un => un.Notification.CreationTime <= endDate.Value)
                .Where(un =>
                    un.UserId == userId
                )
                .OrderByDescending(un=>un.Notification.CreationTime)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<int> GetCountAsync(Guid userId, UserNotificationState? state = null, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(state != null, un => un.State == state)
                .WhereIf(startDate != null, un => un.Notification.CreationTime >= startDate.Value)
                .WhereIf(endDate != null, un => un.Notification.CreationTime <= endDate.Value)
                .Where(un =>
                    un.UserId == userId
                )
                .CountAsync(GetCancellationToken(cancellationToken));
        }


        public async Task<bool> AnyAsync(Guid notificationId, Guid ignoredUserId, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                       .AnyAsync(un => un.NotificationId == notificationId && un.UserId != ignoredUserId, GetCancellationToken(cancellationToken));
        }
    }
}
