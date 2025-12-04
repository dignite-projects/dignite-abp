using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Timing;
using Xunit;

namespace Dignite.Abp.UserPoints;

public class UserPointManager_Tests : UserPointsDomainTestBase
{
    private readonly UserPointManager _userPointsManager;
    private readonly UserPointsTestData _testData;
    private readonly IUserPointRepository _userPointRepository;
    private readonly IClock _clock;

    public UserPointManager_Tests()
    {
        _userPointsManager = GetRequiredService<UserPointManager>();
        _testData = GetRequiredService<UserPointsTestData>();
        _userPointRepository = GetRequiredService<IUserPointRepository>();
        _clock = GetRequiredService<IClock>();
    }

    [Fact]
    public async Task CreateAsync_ShouldWorkProperly()
    {
        var oneYearLater = _clock.Now.AddYears(1);
        var userPoint = await _userPointsManager.CreateAsync(
            _testData.User1Id,
            15, UserPointsTestData.PointType,
            oneYearLater
             );
        await _userPointRepository.InsertAsync(userPoint, true);

        userPoint.Balance.ShouldBe(25);

        userPoint = await _userPointRepository.CalibrateBalanceAsync(_testData.User1Id);
        userPoint.Balance.ShouldBe(25);
        userPoint.NextExpirationAt.ShouldBe(oneYearLater);
    }

    [Fact]
    public async Task ConsumerAsync_ShouldWorkProperly()
    {
        var userPoint = await _userPointsManager.CreateAsync(
            _testData.User1Id,
            -5, UserPointsTestData.PointType
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
                            -15, UserPointsTestData.PointType
                             ));

        exception.ShouldNotBeNull();
        exception.Code.ShouldBe(UserPointsErrorCodes.UserPoint.InsufficientPoint);
    }
}
