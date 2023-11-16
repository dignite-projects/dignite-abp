using Dignite.CmsKit.Favourites;
using Dignite.CmsKit.MongoDB.Favourites;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.CmsKit.MongoDB;

namespace Dignite.CmsKit.MongoDB;

[DependsOn(
    typeof(DigniteCmsKitDomainModule),
    typeof(CmsKitMongoDbModule)
    )]
public class DigniteCmsKitMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<CmsKitMongoDbContext>(options =>
        {
            options.AddRepository<Favourite, MongoFavouriteRepository>();
        });
    }
}
