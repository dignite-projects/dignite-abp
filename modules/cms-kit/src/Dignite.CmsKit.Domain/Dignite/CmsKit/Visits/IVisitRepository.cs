using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.CmsKit.Visits;

public interface IVisitRepository : IBasicRepository<Visit, Guid>
{
    Task<long> GetCountAsync(
        string? entityType=null,
        string? entityId=null,
        string? osName=null,
        Guid? creatorId = null,
        CancellationToken cancellationToken = default
        );

    Task<List<Visit>> GetListAsync(
        string? entityType=null,
        string? entityId = null,
        string? osName = null,
        Guid? creatorId = null,
        int skipCount=0,
        int maxResultCount=100,
        CancellationToken cancellationToken = default
    );

    Task<List<string>> GetEntityIdsFilteredByUserAsync(
        Guid userId,
        [NotNull] string entityType,
        int skipCount = 0,
        int maxResultCount = 100,
        CancellationToken cancellationToken = default
    );
}
