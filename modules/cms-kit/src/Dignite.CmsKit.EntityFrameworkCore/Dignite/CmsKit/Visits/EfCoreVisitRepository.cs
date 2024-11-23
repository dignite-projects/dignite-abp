using System;
using System.Collections.Generic;
using System.Linq;
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

    public async Task<long> GetCountAsync(
        string? entityType=null,
        string? entityId = null,
        string? osName = null, 
        Guid? creatorId = null, 
        CancellationToken cancellationToken = default)
    {
        return await(await GetDbSetAsync())
            .WhereIf(!entityType.IsNullOrEmpty(), v => v.EntityType == entityType)
            .WhereIf(!entityId.IsNullOrEmpty(), v => v.EntityId == entityId)
            .WhereIf(!osName.IsNullOrEmpty(), v => v.DeviceInfo.StartsWith(osName))
            .WhereIf(creatorId.HasValue, v => v.CreatorId == creatorId)
            .CountAsync(GetCancellationToken(cancellationToken));
    }

    public async Task<List<string>> GetEntityIdsFilteredByUserAsync( Guid userId,[NotNull] string entityType, int skipCount = 0, int maxResultCount = 100, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(v => v.EntityType == entityType && v.CreatorId == userId)
            .OrderByDescending(v => v.CreationTime)
            .GroupBy(v => v.EntityId)
            .Select(v => v.Key)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public async Task<List<Visit>> GetListAsync(
        string? entityType = null,
        string? entityId = null,
        string? osName = null,
        Guid? creatorId = null,
        int skipCount = 0,
        int maxResultCount = 100,
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .WhereIf(!entityType.IsNullOrEmpty(), v => v.EntityType == entityType)
            .WhereIf(!entityId.IsNullOrEmpty(), v => v.EntityId == entityId)
            .WhereIf(!osName.IsNullOrEmpty(), v => v.DeviceInfo.StartsWith(osName))
            .WhereIf(creatorId.HasValue, v=>v.CreatorId == creatorId)
            .OrderByDescending(v=>v.CreationTime)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }
}
