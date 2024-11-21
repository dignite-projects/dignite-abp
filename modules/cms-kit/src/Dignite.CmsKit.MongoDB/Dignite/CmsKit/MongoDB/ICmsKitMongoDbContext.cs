using Dignite.CmsKit.Visits;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;
using Volo.CmsKit;

namespace Dignite.CmsKit.MongoDB;

[ConnectionStringName(AbpCmsKitDbProperties.ConnectionStringName)]
public interface ICmsKitMongoDbContext : IAbpMongoDbContext
{
    IMongoCollection<Visit> Visits { get; }
}
