using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.CmsKit.Visits;

public interface IVisitRepository : IBasicRepository<Visit, Guid>
{
    Task<List<Visit>> GetListByUserAsync(
        [NotNull] string entityType,
        Guid userId,
        CancellationToken cancellationToken = default
    );
}
