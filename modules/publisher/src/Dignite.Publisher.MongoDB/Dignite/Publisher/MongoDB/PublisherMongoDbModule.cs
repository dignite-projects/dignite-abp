using Dignite.Publisher.Categories;
using Dignite.Publisher.Posts;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.CmsKit.MongoDB;
using Dignite.FileExplorer.MongoDB;

namespace Dignite.Publisher.MongoDB;

[DependsOn(
    typeof(CmsKitMongoDbModule),
    typeof(FileExplorerMongoDbModule),
    typeof(PublisherDomainModule)
    )]
public class PublisherMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<PublisherMongoDbContext>(options =>
        {
            options.AddRepository<Category, MongoCategoryRepository>();
            options.AddRepository<Post, MongoPostRepository>();
        });
    }
}
