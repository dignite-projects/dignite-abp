using JetBrains.Annotations;
using Volo.Abp.GlobalFeatures;

namespace Dignite.CmsKit.GlobalFeatures;

[GlobalFeatureName(Name)]
public class VisitsFeature : GlobalFeature
{
    public const string Name = "CmsKit.Visits";

    internal VisitsFeature(
        [NotNull] GlobalCmsKitFeatures cmsKit
    ) : base(cmsKit)
    {

    }
}
