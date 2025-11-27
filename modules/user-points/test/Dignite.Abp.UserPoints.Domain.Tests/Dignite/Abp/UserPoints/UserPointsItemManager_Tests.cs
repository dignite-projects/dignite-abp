using System.Threading.Tasks;
using Dignite.Abp.Points;
using RulesEngine.Models;
using Shouldly;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;
using Xunit;

namespace Dignite.Abp.UserPoints;

public class UserPointsItemManager_Tests : UserPointsDomainTestBase
{
    private readonly PointsManager _pointsManager;
    private readonly UserPointsItemManager _userPointsItemManager;
    private readonly UserPointsTestData _testData;
    private readonly IClock _clock;
    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrentTenant _currentTenant;

    public UserPointsItemManager_Tests()
    {
        _pointsManager = GetRequiredService<PointsManager>();
        _userPointsItemManager = GetRequiredService<UserPointsItemManager>();
        _testData = GetRequiredService<UserPointsTestData>();
        _clock = GetRequiredService<IClock>();
        _guidGenerator = GetRequiredService<IGuidGenerator>();
        _currentTenant = GetRequiredService<ICurrentTenant>();
    }

    [Fact]
    public async Task CreateAsync_ShouldWorkProperly()
    {
        var input1 = new RuleParameter("input1", new {
            Authenticated = true
        });
        var input2 = new RuleParameter("input2", new {
            Age = 20
        });

        var points = await _pointsManager.CalculatePointsAsync(
            _testData.PointsDefinitionName,
            _testData.PointsWorkflow1Name,
            null,
            input1,
            input2);

        var userPointsItem = await _userPointsItemManager.CreateAsync(
            PointsType.General,
            _testData.PointsDefinitionName,
            _testData.PointsWorkflow1Name,
            points,
            _clock.Now.AddYears(1),
            _testData.User1Id,
            _currentTenant.Id
             );

        points.ShouldBe(15);
        userPointsItem.Points.ShouldBe(points);
    }
}
