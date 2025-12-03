using System.Threading.Tasks;
using Dignite.Abp.Points;
using RulesEngine.Models;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;

namespace Dignite.Abp.UserPoints;

public class UserPointsDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IClock _clock;
    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrentTenant _currentTenant;
    private readonly UserPointsTestData _testData;
    private readonly IPointsManager _pointsManager;
    private readonly UserPointManager _userPointsManager;
    private readonly IUserPointRepository _userPointsRepository;

    public UserPointsDataSeedContributor(IClock clock, IGuidGenerator guidGenerator, ICurrentTenant currentTenant, UserPointsTestData testData, IPointsManager pointsManager, UserPointManager userPointsItemManager, IUserPointRepository userPointRepository)
    {
        _clock = clock;
        _guidGenerator = guidGenerator;
        _currentTenant = currentTenant;
        _testData = testData;
        _pointsManager = pointsManager;
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
        var input1 = new RuleParameter("input1", new {
            Authenticated = true
        });
        var input2 = new RuleParameter("input2", new {
            Age = 16
        });

        // return 10 points
        var point = await _pointsManager.CalculatePointsAsync(
            _testData.PointsDefinitionName, 
            _testData.PointsWorkflow1Name,
            null, 
            input1, 
            input2);

        var userPoint = await _userPointsManager.CreateAsync(
            _testData.User1Id,
            point,
            _clock.Now.AddYears(1)
             );
        await _userPointsRepository.InsertAsync(userPoint);
    }
}
