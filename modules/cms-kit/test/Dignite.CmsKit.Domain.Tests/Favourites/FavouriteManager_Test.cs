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
    public async Task CreateAsync_ShouldCreate_WhenFirstCall()
    {
        var favourite = await _favouriteManager.CreateAsync(_cmsKitTestData.User1Id, _cmsKitTestData.EntityType1, _cmsKitTestData.EntityId1);

        favourite.ShouldNotBeNull();
        favourite.Id.ShouldNotBe(Guid.Empty);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturn_WithExistingFavourite()
    {
        var favourite = await _favouriteManager.CreateAsync(_cmsKitTestData.User1Id, _cmsKitTestData.EntityType1, _cmsKitTestData.EntityId1);

        favourite.ShouldNotBeNull();
        favourite.Id.ShouldNotBe(Guid.Empty);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowException_WithNotConfiguredentityType()
    {
        var notConfiguredEntityType = "AnyOtherEntityType";

        var exception = await Should.ThrowAsync<EntityCantHaveFavouriteException>(async () =>
                            await _favouriteManager.CreateAsync(_cmsKitTestData.User1Id, notConfiguredEntityType, "1"));

        exception.ShouldNotBeNull();
        exception.EntityType.ShouldBe(notConfiguredEntityType);
    }
}
