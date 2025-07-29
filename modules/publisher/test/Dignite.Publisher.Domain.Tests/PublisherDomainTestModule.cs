using Volo.Abp.Modularity;

namespace Dignite.Publisher;

[DependsOn(
    typeof(PublisherDomainModule),
    typeof(PublisherTestBaseModule)
)]
public class PublisherDomainTestModule : AbpModule
{

}
