using Dignite.CmsKit.Visits;
using Dignite.FileExplorer.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.CmsKit.EntityFrameworkCore;

namespace Dignite.CmsKit.EntityFrameworkCore;

[DependsOn(
    typeof(DigniteCmsKitDomainModule),
    typeof(CmsKitEntityFrameworkCoreModule),
    typeof(FileExplorerEntityFrameworkCoreModule)
)]
public class DigniteCmsKitEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<CmsKitDbContext>(options =>
        {
            options.AddRepository<Visit, EfCoreVisitRepository>();
        });
    }
}
