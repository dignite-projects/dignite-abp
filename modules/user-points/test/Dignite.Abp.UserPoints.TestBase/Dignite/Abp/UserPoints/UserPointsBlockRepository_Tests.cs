using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Modularity;
using Volo.Abp.Timing;
using Xunit;

namespace Dignite.Abp.UserPoints;

/* Write your custom repository tests like that, in this project, as abstract classes.
 * Then inherit these abstract classes from EF Core & MongoDB test projects.
 * In this way, both database providers are tests with the same set tests.
 */
public abstract class UserPointsBlockRepository_Tests<TStartupModule> : UserPointsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IUserPointsBlockRepository _userPointsBlockRepository;
    private readonly UserPointsTestData _testData;

    protected UserPointsBlockRepository_Tests()
    {
        _userPointsBlockRepository = GetRequiredService<IUserPointsBlockRepository>();
        _testData = GetRequiredService<UserPointsTestData>();
    }


    [Fact]
    public async Task CalculatePointsAsync_ShouldWorkProperly_WithUserId_Within2years()
    {
        var result = await _userPointsBlockRepository.GetUserAvailablePointsAsync(_testData.User1Id, null, PointsType.General);

        result.ShouldBe(10);
    }
}
