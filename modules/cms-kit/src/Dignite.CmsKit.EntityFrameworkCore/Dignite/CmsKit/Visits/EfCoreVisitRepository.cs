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


    public async Task<List<Visit>> GetListByUserAsync([NotNull] string entityType, Guid userId, DateTime? visitDate = null, CancellationToken cancellationToken = default)
    {
        return await(await GetDbSetAsync())
            .Where(f => f.EntityType == entityType && f.CreatorId == userId)
            .WhereIf(visitDate.HasValue, v=>v.CreationTime>visitDate.Value.Date && v.CreationTime<visitDate.Value.Date.AddDays(1))
            .ToListAsync(cancellationToken);
    }
}
