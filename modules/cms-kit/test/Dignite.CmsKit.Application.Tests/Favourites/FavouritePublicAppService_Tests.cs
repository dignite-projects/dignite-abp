using System.Linq;
using System.Threading.Tasks;
using Dignite.CmsKit.Public.Favourites;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using Volo.Abp.Users;
using Xunit;

namespace Dignite.CmsKit.Favourites;

public class FavouritePublicAppService_Tests : CmsKitApplicationTestBase
{
    private readonly IFavouritePublicAppService _favouriteAppService;
    private ICurrentUser _currentUser;
    private readonly CmsKitTestData _cmsKitTestData;

    public FavouritePublicAppService_Tests()
    {
        _favouriteAppService = GetRequiredService<IFavouritePublicAppService>();
        _cmsKitTestData = GetRequiredService<CmsKitTestData>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        _currentUser = Substitute.For<ICurrentUser>();
        services.AddSingleton(_currentUser);
    }

    [Fact]
    public async Task CreateAsync()
    {
        _currentUser.Id.Returns(_cmsKitTestData.User1Id);

        var newRating = await _favouriteAppService.CreateAsync(
            _cmsKitTestData.EntityType1,
            _cmsKitTestData.EntityId2);

        UsingDbContext(context =>
        {
            var ratings = context.Set<Favourite>().Where(x =>
                x.EntityId == _cmsKitTestData.EntityId2 && x.EntityType == _cmsKitTestData.EntityType1).ToList();

            ratings
                .Any(c => c.Id == newRating.Id && c.CreatorId == newRating.CreatorId)
                .ShouldBeTrue();
        });
    }

    [Fact]
    public async Task DeleteAsync()
    {
        _currentUser.Id.Returns(_cmsKitTestData.User1Id);

        var rating = await _favouriteAppService.CreateAsync(
            _cmsKitTestData.EntityType1,
            _cmsKitTestData.EntityId1);

        await _favouriteAppService.DeleteAsync(_cmsKitTestData.EntityType1, _cmsKitTestData.EntityId1);

        UsingDbContext(context =>
        {
            var deletedComment = context.Set<Favourite>().FirstOrDefault(x => x.Id == rating.Id);

            deletedComment.ShouldBeNull();
        });
    }

}
