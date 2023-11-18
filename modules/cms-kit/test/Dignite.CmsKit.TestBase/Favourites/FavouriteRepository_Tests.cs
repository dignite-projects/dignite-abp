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
        var userFavourite = await _favouriteRepository.GetCurrentUserAsync(_cmsKitTestData.EntityType1,
            _cmsKitTestData.EntityId1, _cmsKitTestData.User1Id);

        userFavourite.ShouldNotBeNull();
        userFavourite.EntityId.ShouldBe(_cmsKitTestData.EntityId1);
        userFavourite.EntityType.ShouldBe(_cmsKitTestData.EntityType1);
        userFavourite.CreatorId.ShouldBe(_cmsKitTestData.User1Id);
    }
}
