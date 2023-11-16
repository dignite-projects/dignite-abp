using Dignite.CmsKit.Favourites;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;
using Volo.CmsKit;
using Volo.CmsKit.MongoDB;

namespace Dignite.CmsKit.MongoDB;

[ConnectionStringName(AbpCmsKitDbProperties.ConnectionStringName)]
public class CmsKitMongoDbContext : Volo.CmsKit.MongoDB.CmsKitMongoDbContext, ICmsKitMongoDbContext
{
    public IMongoCollection<Favourite> Favourites => Collection<Favourite>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureCmsKit();
        modelBuilder.DigniteConfigureCmsKit();
    }
}
