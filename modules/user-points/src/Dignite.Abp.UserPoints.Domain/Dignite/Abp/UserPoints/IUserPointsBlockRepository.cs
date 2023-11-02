using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Abp.UserPoints;
public interface IUserPointsBlockRepository : IRepository<UserPointsBlock, Guid>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="expirationDate"></param>
    /// <param name="pointsDefinitionName"></param>
    /// <param name="pointsWorkflowName"></param>
    /// <returns></returns>
    Task<int> GetUserAvailablePointsAsync(Guid userId, DateTime? expirationDate=null, string pointsDefinitionName = null, string pointsWorkflowName = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get the points available to the user
    /// </summary>
    /// <param name="top"></param>
    /// <param name="userId"></param>
    /// <param name="pointsDefinitionName"></param>
    /// <param name="pointsWorkflowName"></param>
    /// <returns></returns>
    Task<List<UserPointsBlock>> GetTopAvailableListAsync(int top, Guid userId, string pointsDefinitionName = null, string pointsWorkflowName = null,
        CancellationToken cancellationToken = default);
}
