using Dignite.CmsKit.Favourites;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.CmsKit.MongoDB;

[ConnectionStringName(DigniteCmsKitDbProperties.ConnectionStringName)]
public class CmsKitMongoDbContext : AbpMongoDbContext, ICmsKitMongoDbContext
{
    public IMongoCollection<Favourite> Favourites => Collection<Favourite>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureCmsKit();
    }
}
