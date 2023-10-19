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
    private readonly UserPointsItemManager _userPointsItemManager;
    private readonly IUserPointsOrderRepository _userPointsOrderRepository;

    public UserPointsDataSeedContributor(IClock clock, IGuidGenerator guidGenerator, ICurrentTenant currentTenant, UserPointsTestData testData, IPointsManager pointsManager, UserPointsItemManager userPointsItemManager, IUserPointsOrderRepository userPointsOrderRepository)
    {
        _clock = clock;
        _guidGenerator = guidGenerator;
        _currentTenant = currentTenant;
        _testData = testData;
        _pointsManager = pointsManager;
        _userPointsItemManager = userPointsItemManager;
        _userPointsOrderRepository = userPointsOrderRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await SeedUserPointsItemAsync();
            await SeedUserPointsOrderAsync();
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
        var points = await _pointsManager.CalculatePointsAsync(
            _testData.PointsDefinitionName, 
            _testData.PointsWorkflow1Name,
            null, 
            input1, 
            input2);

        await _userPointsItemManager.CreateAsync(
            _guidGenerator.Create(),
            _testData.PointsDefinitionName,
            _testData.PointsWorkflow1Name,
            PointsType.General,
            points,
            _clock.Now.AddYears(1),
            _testData.User1Id,
            _currentTenant.Id
             );
    }

    private async Task SeedUserPointsOrderAsync()
    {
        await _userPointsOrderRepository.InsertAsync(new UserPointsOrder(
            _guidGenerator.Create(),
            5,
            _testData.BusinessOrderType,
            _testData.BusinessOrderNumber,
            _testData.User1Id,
            _currentTenant.Id));
    }
}
