using System;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.NotificationCenter;

public class NotificationCenterTestData : ISingletonDependency
{
    public Guid User1Id { get; } = Guid.Parse("8C38549B-87F1-1894-60E3-9EC21BD42FAB");

    public string User1UserName => "fake.user";

    public string Notification1Name => "fake.notification";

    public string EntityType1Name => "fake.entitytypename";

    public string Entity1Id => "49526E58-4A83-0786-5947-431543FEE765";
}
