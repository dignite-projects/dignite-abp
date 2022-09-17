using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Abp.NotificationCenter;

public interface INotificationRepository : IBasicRepository<Notification, Guid>
{

    Task<List<Notification>> GetListAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

}