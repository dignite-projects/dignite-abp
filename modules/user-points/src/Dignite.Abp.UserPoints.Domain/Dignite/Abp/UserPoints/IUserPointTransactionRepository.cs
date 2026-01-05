using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Abp.UserPoints;
public interface IUserPointTransactionRepository: IBasicRepository<UserPointTransaction, Guid>
{
    Task<List<UserPointTransaction>> ConsumeAsync(
        Guid userId,
        [ValueRange(int.MinValue, -1)] int amount,
        [CanBeNull] IEnumerable<Guid> accountIds = null,
        [CanBeNull] string entityType = null, [CanBeNull] string entityId = null,
        [CanBeNull] string remark = null,
        CancellationToken cancellationToken = default);

    Task<int> GetCountAsync(
        Guid userId,
        Guid? accountId = null,
        int? minAmount = null,
        int? maxAmount = null,
        DateTime? startExpirationTime = null,
        DateTime? endExpirationTime = null,
        string entityType = null,
        string entityId = null,
        CancellationToken cancellationToken = default
         );
    Task<List<UserPointTransaction>> GetListAsync(
        Guid userId,
        Guid? accountId = null,
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
