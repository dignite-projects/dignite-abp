using JetBrains.Annotations;
using Volo.Abp.GlobalFeatures;

namespace Dignite.Publisher.GlobalFeatures;

[GlobalFeatureName(Name)]
public class ArticlePostsFeature : GlobalFeature
{
    public const string Name = "Publisher.ArticlePosts";

    internal ArticlePostsFeature(
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
