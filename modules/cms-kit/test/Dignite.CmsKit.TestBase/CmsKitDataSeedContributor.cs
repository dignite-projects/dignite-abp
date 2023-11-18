using Dignite.CmsKit.Favourites;
using Dignite.CmsKit.Visits;
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
    private readonly IVisitRepository _visitRepository;
    private readonly ICurrentTenant _currentTenant;
    private readonly IOptions<CmsKitFavouriteOptions> _favouriteOptions;
    private readonly IOptions<CmsKitVisitOptions> _visitOptions;
    private readonly IOptions<CmsKitRatingOptions> _ratingOptions;

    public CmsKitDataSeedContributor(
        IGuidGenerator guidGenerator,
        ICmsUserRepository cmsUserRepository,
        CmsKitTestData cmsKitTestData,
        IFavouriteRepository favouriteRepository,
        IVisitRepository visitRepository,
        ICurrentTenant currentTenant,
        IOptions<CmsKitFavouriteOptions> favouriteOptions,
        IOptions<CmsKitVisitOptions> visitOptions,
        IOptions<CmsKitRatingOptions> ratingOptions)
    {
        _guidGenerator = guidGenerator;
        _cmsUserRepository = cmsUserRepository;
        _cmsKitTestData = cmsKitTestData;
        _favouriteRepository = favouriteRepository;
        _visitRepository = visitRepository;
        _currentTenant = currentTenant;
        _favouriteOptions = favouriteOptions;
        _visitOptions = visitOptions;
        _ratingOptions = ratingOptions;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await ConfigureCmsKitOptionsAsync();


            await SeedUsersAsync();
            await SeedFavouriteAsync();
            await SeedVisitAsync();
        }
    }

    private Task ConfigureCmsKitOptionsAsync()
    {
        _favouriteOptions.Value.EntityTypes.Add(new FavouriteEntityTypeDefinition(_cmsKitTestData.EntityType1));
        _favouriteOptions.Value.EntityTypes.Add(new FavouriteEntityTypeDefinition(_cmsKitTestData.EntityType2));

        _visitOptions.Value.EntityTypes.Add(new VisitEntityTypeDefinition(_cmsKitTestData.EntityType1));
        _visitOptions.Value.EntityTypes.Add(new VisitEntityTypeDefinition(_cmsKitTestData.EntityType2));

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

    private async Task SeedVisitAsync()
    {
        await _visitRepository.InsertAsync(new Visit(_guidGenerator.Create(),
                _cmsKitTestData.EntityType1,
                _cmsKitTestData.EntityId1,
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.36 Edg/119.0.0.0",
                "127.0.0.1",
                60,
                _cmsKitTestData.User1Id
            ));

        await _visitRepository.InsertAsync(new Visit(_guidGenerator.Create(),
            _cmsKitTestData.EntityType2,
            _cmsKitTestData.EntityId2,
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/109.0.5410.0 Safari/537.36",
            "49.245.201.81",
            50,
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
