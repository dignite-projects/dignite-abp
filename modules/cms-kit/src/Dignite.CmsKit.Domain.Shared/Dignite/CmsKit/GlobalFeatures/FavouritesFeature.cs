using JetBrains.Annotations;
using Volo.Abp.GlobalFeatures;

namespace Dignite.CmsKit.GlobalFeatures;

[GlobalFeatureName(Name)]
public class FavouritesFeature : GlobalFeature
{
    public const string Name = "CmsKit.Favourites";

    internal FavouritesFeature(
        [NotNull] GlobalCmsKitFeatures cmsKit
    ) : base(cmsKit)
    {

    }

    public override void Enable()
    {
        base.Enable();
    }
}
