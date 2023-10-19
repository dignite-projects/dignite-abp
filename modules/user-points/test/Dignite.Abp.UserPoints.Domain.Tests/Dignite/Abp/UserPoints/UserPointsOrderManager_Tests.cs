using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Dignite.Abp.UserPoints;
public class UserPointsOrderManager_Tests: UserPointsDomainTestBase
{
    private readonly UserPointsOrderManager _userPointsOrderManager;
    private readonly UserPointsTestData _testData;

    public UserPointsOrderManager_Tests()
    {
        _userPointsOrderManager = GetRequiredService<UserPointsOrderManager>();
        _testData = GetRequiredService<UserPointsTestData>();
    }

    [Fact]
    public async Task CreateAsync_ShouldWorkProperly()
    {
        var order = await _userPointsOrderManager.CreateAsync(
            5,
            _testData.BusinessOrderType,
            _testData.BusinessOrderNumber,
            _testData.User1Id,
            PointsType.General,
            _testData.PointsDefinitionName,
            _testData.PointsWorkflow1Name,
            null);

        order.ShouldNotBeNull();
        order.Points.ShouldBe(5);
    }
}
