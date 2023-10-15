using System.Linq;
using Shouldly;
using Xunit;

namespace Dignite.Abp.Points;
public class PointsDefinitionManager_Tests : PointsTestBase
{
    private readonly IPointsDefinitionManager _pointsDefinitionManager;

    public PointsDefinitionManager_Tests()
    {
        _pointsDefinitionManager = GetRequiredService<IPointsDefinitionManager>();
    }

    [Fact]
    public void Should_Should_Be_Rules_For_Definition_Of_Points()
    {
        var pointsDefinition = _pointsDefinitionManager.GetOrNull("test-points-definition");

        pointsDefinition.ShouldNotBeNull();

        pointsDefinition.Workflows.ShouldNotBeNull();

        pointsDefinition.Workflows.First().Rules.ShouldNotBeNull();
    }
}
