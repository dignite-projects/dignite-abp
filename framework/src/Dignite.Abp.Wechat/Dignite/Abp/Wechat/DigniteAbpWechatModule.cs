using Volo.Abp.Caching;
using Volo.Abp.Modularity;

namespace Dignite.Abp.Wechat;

[DependsOn(
    typeof(AbpCachingModule)
)]
public class DigniteAbpWechatModule : AbpModule
{
}
