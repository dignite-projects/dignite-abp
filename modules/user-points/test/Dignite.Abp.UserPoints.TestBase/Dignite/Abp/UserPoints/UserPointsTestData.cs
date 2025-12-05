using System;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.UserPoints;
public class UserPointsTestData: ISingletonDependency
{
    public Guid User1Id { get; } = Guid.NewGuid();

    public string User1UserName => "fake.user";

    public const int Points = 10;

    public const string PointType = "BlackFridayActivity";

    public const string PointType1 = "BlackFridayActivity1";
}
