using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;

namespace Dignite.Abp.UserPoints;

public class UserPointsDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IClock _clock;
    private readonly ICurrentTenant _currentTenant;
    private readonly UserPointsTestData _testData;
    private readonly UserPointManager _userPointsManager;
    private readonly IUserPointRepository _userPointsRepository;

    public UserPointsDataSeedContributor(IClock clock,  ICurrentTenant currentTenant,
        UserPointsTestData testData, UserPointManager userPointsItemManager, IUserPointRepository userPointRepository)
    {
        _clock = clock;
        _currentTenant = currentTenant;
        _testData = testData;
        _userPointsManager = userPointsItemManager;
        _userPointsRepository = userPointRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await SeedUserPointsItemAsync();
        }
    }

    private async Task SeedUserPointsItemAsync()
    {
        var userPoint = await _userPointsManager.CreateAsync(
            _testData.User1Id,
            10, UserPointsTestData.PointType,
            _clock.Now.AddYears(2)
             );
        await _userPointsRepository.InsertAsync(userPoint);
    }

}
