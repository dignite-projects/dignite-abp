using Dignite.Publisher.Categories;
using Dignite.Publisher.Posts;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.CmsKit.EntityFrameworkCore;

namespace Dignite.Publisher.EntityFrameworkCore;

[DependsOn(
    typeof(PublisherDomainModule),
    typeof(CmsKitEntityFrameworkCoreModule)
)]
public class PublisherEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<PublisherDbContext>(options =>
        {
            options.AddRepository<Category, EfCoreCategoryRepository>();
            options.AddRepository<Post, EfCorePostRepository>();
        });
    }
}
