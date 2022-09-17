using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dignite.Abp.NotificationCenter.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Abp.NotificationCenter;

public class EfCoreNotificationRepository : EfCoreRepository<INotificationCenterDbContext, Notification, Guid>, INotificationRepository
{
    public EfCoreNotificationRepository(
        IDbContextProvider<INotificationCenterDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<List<Notification>> GetListAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(n=> ids.Contains(n.Id))
            .ToListAsync(
                GetCancellationToken(cancellationToken)
            );
    }
}