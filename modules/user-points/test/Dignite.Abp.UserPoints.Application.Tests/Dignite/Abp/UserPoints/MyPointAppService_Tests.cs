using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Dignite.Abp.UserPoints;

public class MyPointAppService_Tests : UserPointsApplicationTestBase
{
    private readonly IMyPointAppService _myPointAppService;

    public MyPointAppService_Tests()
    {
        _myPointAppService = GetRequiredService<IMyPointAppService>();
    }

    [Fact]
    public async Task GetMyPointsAsync()
    {
        var myPoints = await _myPointAppService.GetListAsync(new GetUserPointListInput());

        myPoints.TotalCount.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task CalculatePointsAsync()
    {
        var availablePoint = await _myPointAppService.GetAvailableAsync();

        availablePoint.ShouldBe(10);
    }
}
