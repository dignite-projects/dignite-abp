using Dignite.CmsKit.Favourites;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.CmsKit.MongoDB;

[ConnectionStringName(DigniteCmsKitDbProperties.ConnectionStringName)]
public interface ICmsKitMongoDbContext : IAbpMongoDbContext
{
    IMongoCollection<Favourite> Favourites { get; }
}
