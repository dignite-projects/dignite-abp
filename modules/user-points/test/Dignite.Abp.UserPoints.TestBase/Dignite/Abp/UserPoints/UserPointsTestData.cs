using System;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.UserPoints;
public class UserPointsTestData: ISingletonDependency
{
    public Guid User1Id { get; } = Guid.NewGuid();

    public string User1UserName => "fake.user";

    public string PointsDefinitionName => "BlackFridayActivities";

    public string PointsWorkflow1Name => "DailySignIn";
    public string PointsWorkflow2Name => "ShareToFriends";
    public string BusinessOrderType => "RedeemProduct";
    public string BusinessOrderNumber => "2EACAB78-8FF2-48F6-8CD5-0306F4143AD0";
}
