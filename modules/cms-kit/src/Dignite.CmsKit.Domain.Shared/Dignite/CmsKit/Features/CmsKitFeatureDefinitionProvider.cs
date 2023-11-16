using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.Localization;
using Volo.Abp.Validation.StringValues;
using Dignite.CmsKit.GlobalFeatures;
using Dignite.CmsKit.Localization;

namespace Dignite.CmsKit.Features;
public class CmsKitFeatureDefinitionProvider : FeatureDefinitionProvider
{
    public override void Define(IFeatureDefinitionContext context)
    {
        var group = context.GetGroupOrNull(Volo.CmsKit.Features.CmsKitFeatures.GroupName);

        if (GlobalFeatureManager.Instance.IsEnabled<FavouritesFeature>())
        {
            group.AddFeature(CmsKitFeatures.FavouriteEnable,
            "true",
            L("Feature:FavouriteEnable"),
            L("Feature:FavouriteEnableDescription"),
            new ToggleStringValueType());
        }
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<DigniteCmsKitResource>(name);
    }
}
