using System;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.NotificationCenter;

public class NotificationCenterTestData : ISingletonDependency
{
    public Guid User1Id { get; } = Guid.NewGuid();

    public string User1UserName => "fake.user";

    public string Notification1Name => "fake.notification";

    public string EntityType1Name => "fake.entitytypename";

    public string Entity1Id => "123456";
}
