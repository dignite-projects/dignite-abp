using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace Dignite.CmsKit.Favourites;

public abstract class FavouriteRepository_Tests<TStartupModule> : CmsKitTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly CmsKitTestData _cmsKitTestData;
    private readonly IFavouriteRepository _favouriteRepository;

    public FavouriteRepository_Tests()
    {
        _cmsKitTestData = GetRequiredService<CmsKitTestData>();
        _favouriteRepository = GetRequiredService<IFavouriteRepository>();
    }

    [Fact]
    public async Task GetCurrentUserFavouriteAsync()
    {
        var userRating = await _favouriteRepository.GetAsync(_cmsKitTestData.EntityType1,
            _cmsKitTestData.EntityId1, _cmsKitTestData.User1Id);

        userRating.ShouldNotBeNull();
        userRating.EntityId.ShouldBe(_cmsKitTestData.EntityId1);
        userRating.EntityType.ShouldBe(_cmsKitTestData.EntityType1);
        userRating.CreatorId.ShouldBe(_cmsKitTestData.User1Id);
    }
}
