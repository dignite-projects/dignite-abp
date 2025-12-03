using System.Threading.Tasks;
using Dignite.Abp.Points;
using RulesEngine.Models;
using Shouldly;
using Volo.Abp.Timing;
using Xunit;

namespace Dignite.Abp.UserPoints;

public class UserPointManager_Tests : UserPointsDomainTestBase
{
    private readonly PointsManager _pointsManager;
    private readonly UserPointManager _userPointsManager;
    private readonly UserPointsTestData _testData;
    private readonly IUserPointRepository _userPointRepository;
    private readonly IClock _clock;

    public UserPointManager_Tests()
    {
        _pointsManager = GetRequiredService<PointsManager>();
        _userPointsManager = GetRequiredService<UserPointManager>();
        _testData = GetRequiredService<UserPointsTestData>();
        _userPointRepository = GetRequiredService<IUserPointRepository>();
        _clock = GetRequiredService<IClock>();
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

        points.ShouldBe(15);

        var userPoint = await _userPointsManager.CreateAsync(
            _testData.User1Id,
            points,
            _clock.Now.AddYears(1)
             );
        await _userPointRepository.InsertAsync(userPoint, true);

        userPoint.Balance.ShouldBe(25);

        userPoint = await _userPointRepository.CalibrateBalanceAsync(_testData.User1Id);
        userPoint.Balance.ShouldBe(25);
    }

    [Fact]
    public async Task ConsumerAsync_ShouldWorkProperly()
    {
        var userPoint = await _userPointsManager.CreateAsync(
            _testData.User1Id,
            -5
             );

        userPoint.Balance.ShouldBe(5);
        await _userPointRepository.InsertAsync(userPoint, true);

        userPoint = await _userPointRepository.CalibrateBalanceAsync(_testData.User1Id);
        userPoint.Balance.ShouldBe(5);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowException_WithInsufficientPoint()
    {

        var exception = await Should.ThrowAsync<InsufficientPointException>(async () =>
                            await _userPointsManager.CreateAsync(
                            _testData.User1Id,
                            -15
                             ));

        exception.ShouldNotBeNull();
        exception.Code.ShouldBe(UserPointsErrorCodes.UserPoint.InsufficientPoint);
    }
}
