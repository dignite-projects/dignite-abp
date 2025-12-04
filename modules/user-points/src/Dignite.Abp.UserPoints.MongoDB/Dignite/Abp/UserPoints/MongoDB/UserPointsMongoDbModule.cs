using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace Dignite.Abp.UserPoints.MongoDB;

[DependsOn(
    typeof(UserPointsDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class UserPointsMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<UserPointsMongoDbContext>(options =>
        {
            options.AddRepository<UserPoint, MongoUserPointRepository>();
        });
    }
}
