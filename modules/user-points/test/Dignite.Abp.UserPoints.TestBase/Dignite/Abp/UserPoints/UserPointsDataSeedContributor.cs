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
    private readonly UserPointAccountManager _userPointsManager;

    public UserPointsDataSeedContributor(IClock clock,  ICurrentTenant currentTenant,
        UserPointsTestData testData, UserPointAccountManager userPointsItemManager)
    {
        _clock = clock;
        _currentTenant = currentTenant;
        _testData = testData;
        _userPointsManager = userPointsItemManager;
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
        await _userPointsManager.EarnAsync(
            _testData.User1Id,
            UserPointsTestData.Points, 
            UserPointsTestData.PointType,
            _clock.Now.AddYears(2)
             );
        await _userPointsManager.EarnAsync(
            _testData.User1Id,
            UserPointsTestData.Points,
            UserPointsTestData.PointType1
             );
    }

}
