using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dignite.CmsKit.Favourites;
using JetBrains.Annotations;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace Dignite.CmsKit.MongoDB.Favourites;

public class MongoFavouriteRepository : MongoDbRepository<ICmsKitMongoDbContext, Favourite, Guid>, IFavouriteRepository
{
    public MongoFavouriteRepository(IMongoDbContextProvider<ICmsKitMongoDbContext> dbContextProvider) : base(
        dbContextProvider)
    {
    }

    public virtual async Task<Favourite> GetAsync(string entityType, string entityId, Guid userId,
        CancellationToken cancellationToken = default)
    {
        Check.NotNullOrWhiteSpace(entityType, nameof(entityType));
        Check.NotNullOrWhiteSpace(entityId, nameof(entityId));

        var favourite = await (await GetMongoQueryableAsync(cancellationToken))
            .FirstOrDefaultAsync(r => r.EntityType == entityType && r.EntityId == entityId && r.CreatorId == userId,
                GetCancellationToken(cancellationToken));

        return favourite;
    }

    public virtual async Task<List<Favourite>> GetListByUserAsync([NotNull] string entityType, Guid userId, CancellationToken cancellationToken = default)
    {
        return await (await GetMongoQueryableAsync(cancellationToken))
            .Where(r => r.EntityType == entityType && r.CreatorId == userId)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }
}
