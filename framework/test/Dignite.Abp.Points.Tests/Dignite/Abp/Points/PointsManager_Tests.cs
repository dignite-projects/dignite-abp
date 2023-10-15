using System.Threading.Tasks;
using RulesEngine.Models;
using Shouldly;
using Xunit;

namespace Dignite.Abp.Points;
public class PointsManager_Tests : PointsTestBase
{
    private readonly IPointsManager _pointsManager;

    public PointsManager_Tests()
    {
        _pointsManager = GetRequiredService<IPointsManager>();
    }

    [Fact]
    public async Task Should_Should_Be_Rules_For_Definition_Of_Points()
    {
        var input1 = new RuleParameter("input1",new {
            Authenticated = true
        });
        var input2 = new RuleParameter("input2", new {
            Age = 16
        });

        var points = await _pointsManager.PointsCalculationAsync(
            "test-points-definition", 
            "test-points-workflow", 
            null,
            input1, input2
            );

        points.ShouldBe(10);
    }
}
