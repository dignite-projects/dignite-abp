using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Timing;
using Xunit;

namespace Dignite.Abp.UserPoints;

public class UserPointTransactionManager_Tests : UserPointsDomainTestBase
{
    private readonly UserPointAccountManager _userPointsManager;
    private readonly UserPointsTestData _testData;
    private readonly IUserPointAccountRepository _accountRepository;
    private readonly IClock _clock;

    public UserPointTransactionManager_Tests()
    {
        _userPointsManager = GetRequiredService<UserPointAccountManager>();
        _testData = GetRequiredService<UserPointsTestData>();
        _accountRepository = GetRequiredService<IUserPointAccountRepository>();
        _clock = GetRequiredService<IClock>();
    }

    [Fact]
    public async Task EarnAsync_ShouldWorkProperly()
    {
        var oneYearLater = _clock.Now.AddYears(1);
        var account = await _userPointsManager.EarnAsync(
            _testData.User1Id,
            15, 
            UserPointsTestData.PointType1
             );

        account.CurrentBalance.ShouldBe(25);

        var otherAccount = await _userPointsManager.EarnAsync(
            _testData.User1Id,
            10,
            UserPointsTestData.PointType,
            oneYearLater
             );

        otherAccount.CurrentBalance.ShouldBe(20);

        var accounts = await _accountRepository.GetListAsync(a=>a.UserId==_testData.User1Id);
        accounts.Sum(a=>a.CurrentBalance).ShouldBe(45);
    }

    [Fact]
    public async Task ConsumerAsync_ShouldWorkProperly()
    {
        var accounts = await _userPointsManager.ConsumeAsync(
            _testData.User1Id,
            -5
             );

        accounts.Sum(a => a.CurrentBalance).ShouldBe(15);
    }

    [Fact]
    public async Task ConsumeAsync_ShouldThrowException_WithInsufficientPoint()
    {
        var exception = await Should.ThrowAsync<InsufficientPointException>(async () =>
                            await _userPointsManager.ConsumeAsync(
                            _testData.User1Id, 
                            -150
                             ));

        exception.ShouldNotBeNull();
        exception.Code.ShouldBe(UserPointsErrorCodes.UserPoint.InsufficientPoint);
    }
}
