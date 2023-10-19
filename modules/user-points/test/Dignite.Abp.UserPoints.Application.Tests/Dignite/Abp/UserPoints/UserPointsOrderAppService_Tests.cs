using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Dignite.Abp.UserPoints;

public class UserPointsOrderAppService_Tests : UserPointsApplicationTestBase
{
    private readonly IUserPointsOrderAppService _userPointsOrderAppService;
    private readonly UserPointsTestData _testData;

    public UserPointsOrderAppService_Tests()
    {
        _userPointsOrderAppService = GetRequiredService<IUserPointsOrderAppService>();
        _testData = GetRequiredService<UserPointsTestData>();
    }

    [Fact]
    public async Task GetMyOrdersAsync()
    {
        var myPoints = await _userPointsOrderAppService.GetMyOrdersAsync(new GetMyOrdersInput());

        myPoints.TotalCount.ShouldBeGreaterThan(0);
        myPoints.Items.Any(x => x.BusinessOrderNumber== _testData.BusinessOrderNumber).ShouldBeTrue();
    }
}
