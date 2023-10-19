using System;
using System.Threading.Tasks;
using System.Threading;
using Volo.Abp.Domain.Repositories;
using System.Collections.Generic;

namespace Dignite.Abp.UserPoints;
public interface IUserPointsOrderRepository : IBasicRepository<UserPointsOrder, Guid>
{
    Task<int> GetCountAsync(
        Guid userId,
        DateTime? startTime = null,
        DateTime? endTime = null,
        CancellationToken cancellationToken = default
         );
    Task<List<UserPointsOrder>> GetListAsync(
        Guid userId,
        DateTime? startTime = null,
        DateTime? endTime = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
         );
}
