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
public abstract class UserPointTransactionRepository_Tests<TStartupModule> : UserPointsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IUserPointTransactionRepository _userPointsItemRepository;
    private readonly UserPointsTestData _testData;
    private readonly IClock _clock;

    protected UserPointTransactionRepository_Tests()
    {
        _userPointsItemRepository = GetRequiredService<IUserPointTransactionRepository>();
        _testData = GetRequiredService<UserPointsTestData>();
        _clock = GetRequiredService<IClock>();
    }


    [Fact]
    public async Task GetPagedListAsync_ShouldWorkProperly_WithUserId_WhileGetting10_WithoutSorting()
    {
        var result = await _userPointsItemRepository.GetListAsync(_testData.User1Id);

        result.Count.ShouldBe(2);
    }
}
