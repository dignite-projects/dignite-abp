using JetBrains.Annotations;
using Volo.Abp.GlobalFeatures;

namespace Dignite.CmsKit.GlobalFeatures;

public class GlobalCmsKitFeatures : GlobalModuleFeatures
{
    public const string ModuleName = "DigniteCmsKit";
    public FavouritesFeature Favourites => GetFeature<FavouritesFeature>();
    public VisitsFeature Visits => GetFeature<VisitsFeature>();

    public GlobalCmsKitFeatures([NotNull] GlobalFeatureManager featureManager)
        : base(featureManager)
    {
        AddFeature(new FavouritesFeature(this));
        AddFeature(new VisitsFeature(this));
    }
}
