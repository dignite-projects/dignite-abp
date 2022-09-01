using Dignite.Abp.NotificationCenter.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Abp.NotificationCenter
{
    public class EfCoreNotificationRepository : EfCoreRepository<INotificationCenterDbContext, Notification,Guid>, INotificationRepository
    {
        public EfCoreNotificationRepository(
            IDbContextProvider<INotificationCenterDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
