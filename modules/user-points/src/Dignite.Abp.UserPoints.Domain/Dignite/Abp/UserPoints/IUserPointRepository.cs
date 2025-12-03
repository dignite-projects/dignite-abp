using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Abp.UserPoints;
public interface IUserPointRepository: IBasicRepository<UserPoint, Guid>
{
    /// <summary>
    /// Recalculate the points balance for the specified user and return the updated user points information.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose balance is to be recalibrated.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a UserPoint object with the updated
    /// balance information for the user.</returns>
    Task<UserPoint> CalibrateBalanceAsync(
        Guid userId,
        CancellationToken cancellationToken = default
         );

    Task<int> GetCountAsync(
        Guid userId,
        string pointType = null,
        int? minAmount = null,
        int? maxAmount = null,
        DateTime? startExpirationTime = null,
        DateTime? endExpirationTime = null,
        string entityType = null,
        string entityId = null,
        CancellationToken cancellationToken = default
         );
    Task<List<UserPoint>> GetListAsync(
        Guid userId,
        string pointType = null,
        int? minAmount = null,
        int? maxAmount = null,
        DateTime? startExpirationTime = null,
        DateTime? endExpirationTime = null,
        string entityType = null,
        string entityId = null,
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        string sorting = null,
        CancellationToken cancellationToken = default
         );
}
