using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Timing;
using Xunit;

namespace Dignite.Abp.UserPoints;

public class UserPointsItemAppService_Tests : UserPointsApplicationTestBase
{
    private readonly IUserPointsItemAppService _userPointsItemAppService;
    private readonly IClock _clock;

    public UserPointsItemAppService_Tests()
    {
        _userPointsItemAppService = GetRequiredService<IUserPointsItemAppService>();
        _clock = GetRequiredService<IClock>();
    }

    [Fact]
    public async Task GetMyPointsAsync()
    {
        var myPoints = await _userPointsItemAppService.GetListAsync(new GetUserPointsItemsInput());

        myPoints.TotalCount.ShouldBeGreaterThan(0);
        myPoints.Items.Any(x => x.PointsType== PointsType.General).ShouldBeTrue();
    }

    [Fact]
    public async Task CalculatePointsAsync()
    {
        var points = await _userPointsItemAppService.GetTotalPointsAsync(new GetUserTotalPointsInput(_clock.Now.AddYears(3)));

        points.ShouldBeGreaterThan(0);
    }
}
