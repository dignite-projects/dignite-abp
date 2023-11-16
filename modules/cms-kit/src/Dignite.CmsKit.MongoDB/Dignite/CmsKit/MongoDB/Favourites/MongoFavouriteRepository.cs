using System;
using System.Threading;
using System.Threading.Tasks;
using Dignite.CmsKit.Favourites;
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

    public virtual async Task<Favourite> GetFavouriteAsync(string entityType, string entityId, Guid userId,
        CancellationToken cancellationToken = default)
    {
        Check.NotNullOrWhiteSpace(entityType, nameof(entityType));
        Check.NotNullOrWhiteSpace(entityId, nameof(entityId));

        var favourite = await (await GetMongoQueryableAsync(cancellationToken))
            .FirstOrDefaultAsync(r => r.EntityType == entityType && r.EntityId == entityId && r.CreatorId == userId,
                GetCancellationToken(cancellationToken));

        return favourite;
    }
}
