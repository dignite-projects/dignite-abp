using Dignite.Publisher.GlobalFeatures;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.Localization;
using Volo.Abp.Validation.StringValues;
using Volo.CmsKit.Localization;

namespace Dignite.Publisher.Features;
public class PublisherFeatureDefinitionProvider : FeatureDefinitionProvider
{
    public override void Define(IFeatureDefinitionContext context)
    {
        var group = context.AddGroup(PublisherFeatures.GroupName,
            L("Feature:PublisherGroup"));

        if (GlobalFeatureManager.Instance.IsEnabled<ArticlePostsFeature>())
        {
            group.AddFeature(PublisherFeatures.ArticlePostEnable,
            "true",
            L("Feature:ArticlePostEnable"),
            L("Feature:ArticlePostEnableDescription"),
            new ToggleStringValueType());
        }

        if (GlobalFeatureManager.Instance.IsEnabled<VideoPostsFeature>())
        {
            group.AddFeature(PublisherFeatures.VideoPostEnable,
            "true",
            L("Feature:VideoPostEnable"),
            L("Feature:VideoPostEnableDescription"),
            new ToggleStringValueType());
        }
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CmsKitResource>(name);
    }
}
