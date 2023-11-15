using Dignite.CmsKit.Favourites;
using Dignite.CmsKit.MongoDB.Favourites;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace Dignite.CmsKit.MongoDB;

[DependsOn(
    typeof(DigniteCmsKitDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class CmsKitMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<CmsKitMongoDbContext>(options =>
        {
            options.AddRepository<Favourite, MongoFavouriteRepository>();
        });
    }
}
