using Dignite.CmsKit.Visits;
using Dignite.FileExplorer.MongoDB;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;
using Volo.CmsKit;
using Volo.CmsKit.MongoDB;

namespace Dignite.CmsKit.MongoDB;

[ConnectionStringName(AbpCmsKitDbProperties.ConnectionStringName)]
public class CmsKitMongoDbContext : Volo.CmsKit.MongoDB.CmsKitMongoDbContext, ICmsKitMongoDbContext
{
    public IMongoCollection<Visit> Visits => Collection<Visit>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureFileExplorer();
        modelBuilder.ConfigureCmsKit();
        modelBuilder.DigniteConfigureCmsKit();
    }
}
