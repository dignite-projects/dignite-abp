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

        if (GlobalFeatureManager.Instance.IsEnabled<VisitsFeature>())
        {
            group.AddFeature(CmsKitFeatures.VisitEnable,
            "true",
            L("Feature:VisitEnable"),
            L("Feature:VisitEnableDescription"),
            new ToggleStringValueType());
        }
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<DigniteCmsKitResource>(name);
    }
}
