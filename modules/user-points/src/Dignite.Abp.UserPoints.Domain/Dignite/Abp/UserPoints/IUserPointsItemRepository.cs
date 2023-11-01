using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Abp.UserPoints;
public interface IUserPointsItemRepository: IBasicRepository<UserPointsItem, Guid>
{
    Task<int> GetCountAsync(
        Guid userId,
        PointsType pointsType = PointsType.General,
        string pointsDefinitionName = null,
        string pointsWorkflowName = null,
        DateTime? startTime = null,
        DateTime? EndTime = null,
        CancellationToken cancellationToken = default
         );
    Task<List<UserPointsItem>> GetListAsync(
        Guid userId,
        PointsType pointsType = PointsType.General,
        string pointsDefinitionName = null,
        string pointsWorkflowName = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
         );
}
