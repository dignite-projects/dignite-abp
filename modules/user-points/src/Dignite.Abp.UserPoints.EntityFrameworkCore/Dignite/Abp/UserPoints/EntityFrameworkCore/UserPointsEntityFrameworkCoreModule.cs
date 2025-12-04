using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Dignite.Abp.UserPoints.EntityFrameworkCore;

[DependsOn(
    typeof(UserPointsDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class UserPointsEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<UserPointsDbContext>(options =>
        {
            options.AddRepository<UserPoint, EfCoreUserPointRepository>();
        });
    }
}
