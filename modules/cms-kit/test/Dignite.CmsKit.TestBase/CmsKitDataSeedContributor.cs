using Dignite.CmsKit.Favourites;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Users;
using Volo.CmsKit.Ratings;
using Volo.CmsKit.Users;

namespace Dignite.CmsKit;

public class CmsKitDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly ICmsUserRepository _cmsUserRepository;
    private readonly CmsKitTestData _cmsKitTestData;
    private readonly IFavouriteRepository _favouriteRepository;
    private readonly ICurrentTenant _currentTenant;
    private readonly IOptions<CmsKitFavouriteOptions> _favouriteOptions;
    private readonly IOptions<CmsKitRatingOptions> _ratingOptions;

    public CmsKitDataSeedContributor(
        IGuidGenerator guidGenerator,
        ICmsUserRepository cmsUserRepository,
        CmsKitTestData cmsKitTestData,
        IFavouriteRepository favouriteRepository,
        ICurrentTenant currentTenant,
        IOptions<CmsKitFavouriteOptions> favouriteOptions,
        IOptions<CmsKitRatingOptions> ratingOptions)
    {
        _guidGenerator = guidGenerator;
        _cmsUserRepository = cmsUserRepository;
        _cmsKitTestData = cmsKitTestData;
        _favouriteRepository = favouriteRepository;
        _currentTenant = currentTenant;
        _favouriteOptions = favouriteOptions;
        _ratingOptions = ratingOptions;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await ConfigureCmsKitOptionsAsync();


            await SeedUsersAsync();
            await SeedFavouriteAsync();
        }
    }

    private Task ConfigureCmsKitOptionsAsync()
    {
        _favouriteOptions.Value.EntityTypes.Add(new FavouriteEntityTypeDefinition(_cmsKitTestData.EntityType1));
        _favouriteOptions.Value.EntityTypes.Add(new FavouriteEntityTypeDefinition(_cmsKitTestData.EntityType2));

        _ratingOptions.Value.EntityTypes.Add(new RatingEntityTypeDefinition(_cmsKitTestData.EntityType1));
        _ratingOptions.Value.EntityTypes.Add(new RatingEntityTypeDefinition(_cmsKitTestData.EntityType2));

        return Task.CompletedTask;
    }


    private async Task SeedFavouriteAsync()
    {
        await _favouriteRepository.InsertAsync(new Favourite(_guidGenerator.Create(),
                _cmsKitTestData.EntityType1,
                _cmsKitTestData.EntityId1,
                _cmsKitTestData.User1Id
            ));

        await _favouriteRepository.InsertAsync(new Favourite(_guidGenerator.Create(),
            _cmsKitTestData.EntityType2,
            _cmsKitTestData.EntityId2,
            _cmsKitTestData.User2Id
        ));
    }

    private async Task SeedUsersAsync()
    {
        await _cmsUserRepository.InsertAsync(new CmsUser(new UserData(_cmsKitTestData.User1Id, "user1",
            "user1@volo.com",
            "user", "1")),
            autoSave: true);

        await _cmsUserRepository.InsertAsync(new CmsUser(new UserData(_cmsKitTestData.User2Id, "user2",
            "user2@volo.com",
            "user", "2")),
            autoSave: true);
    }
    
}
