using Dignite.CmsKit.Favourites;
using Dignite.CmsKit.Visits;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;
using Volo.CmsKit;

namespace Dignite.CmsKit.MongoDB;

[ConnectionStringName(AbpCmsKitDbProperties.ConnectionStringName)]
public interface ICmsKitMongoDbContext : IAbpMongoDbContext
{
    IMongoCollection<Favourite> Favourites { get; }
    IMongoCollection<Visit> Visits { get; }
}
