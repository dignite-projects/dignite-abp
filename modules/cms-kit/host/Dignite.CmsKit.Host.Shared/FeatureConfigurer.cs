using Volo.Abp.GlobalFeatures;
using Volo.Abp.Threading;

namespace Dignite.CmsKit
{
    public static class FeatureConfigurer
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            OneTimeRunner.Run(() =>
            {
                GlobalFeatureManager.Instance.Modules.DigniteCmsKit().EnableAll();
            });
        }
    }
}
