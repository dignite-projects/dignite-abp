using JetBrains.Annotations;
using Volo.Abp.GlobalFeatures;

namespace Dignite.CmsKit.GlobalFeatures;

public class GlobalCmsKitFeatures : Volo.CmsKit.GlobalFeatures.GlobalCmsKitFeatures
{
    public FavouritesFeature Favourites => GetFeature<FavouritesFeature>();

    public GlobalCmsKitFeatures([NotNull] GlobalFeatureManager featureManager)
        : base(featureManager)
    {
        AddFeature(new FavouritesFeature(this));
    }
}
