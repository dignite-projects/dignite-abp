using Dignite.CmsKit.Favourites;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.CmsKit.EntityFrameworkCore;

namespace Dignite.CmsKit.EntityFrameworkCore;

[DependsOn(
    typeof(DigniteCmsKitDomainModule),
    typeof(CmsKitEntityFrameworkCoreModule)
)]
public class DigniteCmsKitEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<CmsKitDbContext>(options =>
        {
            options.AddRepository<Favourite, EfCoreFavouriteRepository>();
        });
    }
}
