using JetBrains.Annotations;
using Volo.Abp.GlobalFeatures;

namespace Dignite.Publisher.GlobalFeatures;
public class GlobalPublisherFeatures : GlobalModuleFeatures
{
    public const string ModuleName = "Publisher";

    public GlobalPublisherFeatures([NotNull] GlobalFeatureManager featureManager)
        : base(featureManager)
    {
        AddFeature(new ArticlePostsFeature(this));
        AddFeature(new VideoPostsFeature(this));
    }

    public ArticlePostsFeature ArticlePosts => GetFeature<ArticlePostsFeature>();

    public VideoPostsFeature VideoPosts => GetFeature<VideoPostsFeature>();
}
