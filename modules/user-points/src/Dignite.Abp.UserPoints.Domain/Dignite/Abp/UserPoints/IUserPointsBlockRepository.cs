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
    /// <param name="pointsType">
    /// If the value is <see cref="PointsType.Specialized"/>, the values of pointsDefinitionName and pointsWorkflowName must be specified. 
    /// </param>
    /// <param name="pointsDefinitionName"></param>
    /// <param name="pointsWorkflowName"></param>
    /// <returns></returns>
    Task<int> GetUserAvailablePointsAsync(Guid userId, DateTime? expirationDate=null, PointsType pointsType = PointsType.General, string pointsDefinitionName = null, string pointsWorkflowName = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get the points available to the user
    /// </summary>
    /// <param name="top"></param>
    /// <param name="userId"></param>
    /// <param name="pointsType">
    /// If the value is <see cref="PointsType.Specialized"/>, the values of pointsDefinitionName and pointsWorkflowName must be specified. 
    /// </param>
    /// <param name="pointsDefinitionName"></param>
    /// <param name="pointsWorkflowName"></param>
    /// <returns></returns>
    Task<List<UserPointsBlock>> GetTopAvailableListAsync(int top, Guid userId, PointsType pointsType = PointsType.General, string pointsDefinitionName = null, string pointsWorkflowName = null,
        CancellationToken cancellationToken = default);
}
