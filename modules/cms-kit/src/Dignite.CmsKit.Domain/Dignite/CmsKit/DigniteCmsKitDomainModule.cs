using Dignite.CmsKit.Favourites;
using Dignite.CmsKit.Visits;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Modularity;
using Volo.CmsKit;

namespace Dignite.CmsKit;

[DependsOn(
    typeof(CmsKitDomainModule),
    typeof(DigniteCmsKitDomainSharedModule),
    typeof(AbpAutoMapperModule)
)]
public class DigniteCmsKitDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<DigniteCmsKitDomainModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<CmsKitDomainMappingProfile>(validate: true);
        });

        Configure<AbpDistributedEntityEventOptions>(options =>
        {
            options.EtoMappings.Add<Favourite, FavouriteEto>();
            options.EtoMappings.Add<Visit, VisitEto>();

            options.AutoEventSelectors.Add<Favourite>();
            options.AutoEventSelectors.Add<Visit>();
        });

    }
}
