using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Dignite.CmsKit.EntityFrameworkCore;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.CmsKit.Visits;

public class EfCoreVisitRepository : EfCoreRepository<ICmsKitDbContext, Visit, Guid>, IVisitRepository
{
    public EfCoreVisitRepository(IDbContextProvider<ICmsKitDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }


    public async Task<List<string>> GetEntityIdsListByUserAsync([NotNull] string entityType, Guid userId, DateTime? visitDate = null, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(v => v.EntityType == entityType && v.CreatorId == userId)
            .WhereIf(visitDate.HasValue, v => v.CreationTime > visitDate.Value.Date && v.CreationTime < visitDate.Value.Date.AddDays(1))
            .OrderByDescending(v=>v.CreationTime)
            .GroupBy(v => v.EntityId)
            .Select(v => v.Key)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }
}
