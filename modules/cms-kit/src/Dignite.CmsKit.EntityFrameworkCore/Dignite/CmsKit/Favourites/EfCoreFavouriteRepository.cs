using Dignite.CmsKit.EntityFrameworkCore;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq;

namespace Dignite.CmsKit.Favourites;

public class EfCoreFavouriteRepository : EfCoreRepository<ICmsKitDbContext, Favourite, Guid>, IFavouriteRepository
{
    public EfCoreFavouriteRepository(IDbContextProvider<ICmsKitDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task<Favourite> GetCurrentUserAsync(string entityType, string entityId, Guid userId,
        CancellationToken cancellationToken = default)
    {
        Check.NotNullOrWhiteSpace(entityType, nameof(entityType));
        Check.NotNullOrWhiteSpace(entityId, nameof(entityId));

        var favourite = await (await GetDbSetAsync()).FirstOrDefaultAsync(
            r => r.EntityType == entityType && r.EntityId == entityId && r.CreatorId == userId,
            GetCancellationToken(cancellationToken));

        return favourite;
    }

    public async Task<List<Favourite>> GetListByUserAsync([NotNull] string entityType, Guid userId, CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(f => f.EntityType == entityType && f.CreatorId == userId)
            .ToListAsync(cancellationToken);
    }
}
