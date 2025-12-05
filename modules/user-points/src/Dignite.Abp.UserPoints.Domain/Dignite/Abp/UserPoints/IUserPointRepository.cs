using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
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

    /// <summary>
    /// Consumes resources for the specified user that are set to expire, up to the given amount, in an asynchronous
    /// operation.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose expiring resources are to be consumed.</param>
    /// <param name="amount">The maximum number of expiring resources to consume. Must be less than or equal to -1.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous consume operation.</returns>
    Task ConsumeByExpirationAsync(
        Guid userId,
        [ValueRange(int.MinValue, -1)] int amount,
        CancellationToken cancellationToken = default);

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
