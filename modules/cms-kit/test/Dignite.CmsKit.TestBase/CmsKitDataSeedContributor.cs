using System.Threading.Tasks;
using Dignite.CmsKit.Visits;
using Microsoft.Extensions.Options;
using NSubstitute;
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
    private readonly IVisitRepository _visitRepository;
    private readonly ICurrentTenant _currentTenant;
    private readonly ICurrentUser _currentUser;
    private readonly IOptions<CmsKitVisitOptions> _visitOptions;
    private readonly IOptions<CmsKitRatingOptions> _ratingOptions;

    public CmsKitDataSeedContributor(
        IGuidGenerator guidGenerator,
        ICmsUserRepository cmsUserRepository,
        CmsKitTestData cmsKitTestData,
        IVisitRepository visitRepository,
        ICurrentTenant currentTenant,
        ICurrentUser currentUser,
        IOptions<CmsKitVisitOptions> visitOptions,
        IOptions<CmsKitRatingOptions> ratingOptions)
    {
        _guidGenerator = guidGenerator;
        _cmsUserRepository = cmsUserRepository;
        _cmsKitTestData = cmsKitTestData;
        _visitRepository = visitRepository;
        _currentTenant = currentTenant;
        _currentUser = currentUser;
        _visitOptions = visitOptions;
        _ratingOptions = ratingOptions;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await ConfigureCmsKitOptionsAsync();


            await SeedUsersAsync();
            await SeedVisitAsync();
        }
    }

    private Task ConfigureCmsKitOptionsAsync()
    {
        _visitOptions.Value.EntityTypes.Add(new VisitEntityTypeDefinition(_cmsKitTestData.EntityType1));
        _visitOptions.Value.EntityTypes.Add(new VisitEntityTypeDefinition(_cmsKitTestData.EntityType2));

        _ratingOptions.Value.EntityTypes.Add(new RatingEntityTypeDefinition(_cmsKitTestData.EntityType1));
        _ratingOptions.Value.EntityTypes.Add(new RatingEntityTypeDefinition(_cmsKitTestData.EntityType2));

        return Task.CompletedTask;
    }


    private async Task SeedVisitAsync()
    {
        await _visitRepository.InsertAsync(new Visit(_guidGenerator.Create(),
                _cmsKitTestData.EntityType1,
                _cmsKitTestData.EntityId1,
                "Mozilla/5.0 (iPhone 6s; CPU iPhone OS 11_4_1 like Mac OS X) AppleWebKit/604.3.5 (KHTML, like Gecko) Version/11.0 MQQBrowser/8.3.0 Mobile/15B87 Safari/604.1 MttCustomUA/2 QBWebViewType/1 WKType/1",
                "ios",
                "127.0.0.1",
                60
            ));

        await _visitRepository.InsertAsync(new Visit(_guidGenerator.Create(),
            _cmsKitTestData.EntityType2,
            _cmsKitTestData.EntityId2,
            "Mozilla/5.0 (Linux; Android 7.1.1; OPPO R9sk) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.111 Mobile Safari/537.36",
            "android",
            "49.245.201.81",
            50
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
