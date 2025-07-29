using JetBrains.Annotations;
using Volo.Abp.GlobalFeatures;

namespace Dignite.Publisher.GlobalFeatures;

[GlobalFeatureName(Name)]
public class VideoPostsFeature : GlobalFeature
{
    public const string Name = "Publisher.VideoPosts";

    internal VideoPostsFeature(
        [NotNull] GlobalPublisherFeatures publisher
    ) : base(publisher)
    {
    }

    public override void Enable()
    {
        var userFeature = FeatureManager.Modules.CmsKit().User;
        if (!userFeature.IsEnabled)
        {
            userFeature.Enable();
        }

        base.Enable();
    }
}
