using Dignite.CmsKit.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.CmsKit.Favourites;

public class EfCoreFavouriteRepository : EfCoreRepository<ICmsKitDbContext, Favourite, Guid>, IFavouriteRepository
{
    public EfCoreFavouriteRepository(IDbContextProvider<ICmsKitDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task<Favourite> GetFavouriteAsync(string entityType, string entityId, Guid userId,
        CancellationToken cancellationToken = default)
    {
        Check.NotNullOrWhiteSpace(entityType, nameof(entityType));
        Check.NotNullOrWhiteSpace(entityId, nameof(entityId));

        var favourite = await (await GetDbSetAsync()).FirstOrDefaultAsync(
            r => r.EntityType == entityType && r.EntityId == entityId && r.CreatorId == userId,
            GetCancellationToken(cancellationToken));

        return favourite;
    }

}
