using Volo.Abp.Modularity;

namespace Dignite.Publisher;

[DependsOn(
    typeof(PublisherApplicationModule),
    typeof(PublisherDomainTestModule)
    )]
public class PublisherApplicationTestModule : AbpModule
{

}
