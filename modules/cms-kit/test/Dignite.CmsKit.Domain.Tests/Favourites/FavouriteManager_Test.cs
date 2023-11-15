using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Dignite.CmsKit.Favourites;

public class FavouriteManager_Test : CmsKitDomainTestBase
{
    private readonly CmsKitTestData _cmsKitTestData;
    private readonly FavouriteManager _favouriteManager;

    public FavouriteManager_Test()
    {
        _cmsKitTestData = GetRequiredService<CmsKitTestData>();
        _favouriteManager = GetRequiredService<FavouriteManager>();
    }

    [Fact]
    public async Task SetStarAsync_ShouldCreate_WhenFirstCall()
    {
        var favourite = await _favouriteManager.SetStarAsync(_cmsKitTestData.User1Id, _cmsKitTestData.EntityType1, _cmsKitTestData.EntityId1);

        favourite.ShouldNotBeNull();
        favourite.Id.ShouldNotBe(Guid.Empty);
    }

    [Fact]
    public async Task SetStarAsync_ShouldUpdate_WithExistingRating()
    {
        var favourite = await _favouriteManager.SetStarAsync(_cmsKitTestData.User1Id, _cmsKitTestData.EntityType1, _cmsKitTestData.EntityId1);

        favourite.ShouldNotBeNull();
        favourite.Id.ShouldNotBe(Guid.Empty);
    }

    [Fact]
    public async Task SetStarAsync_ShouldThrowException_WithNotConfiguredentityType()
    {
        var notConfiguredEntityType = "AnyOtherEntityType";

        var exception = await Should.ThrowAsync<EntityCantHaveFavouriteException>(async () =>
                            await _favouriteManager.SetStarAsync(_cmsKitTestData.User1Id, notConfiguredEntityType, "1"));

        exception.ShouldNotBeNull();
        exception.EntityType.ShouldBe(notConfiguredEntityType);
    }
}
