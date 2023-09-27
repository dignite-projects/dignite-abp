using Dignite.Abp.NotificationCenter.MongoDB;
using Xunit;

namespace Dignite.Abp.NotificationCenter;

[Collection(MongoTestCollection.Name)]
public class NotificationSubscriptionRepository_Tests : NotificationSubscriptionRepository_Tests<DigniteAbpNotificationCenterMongoDbTestModule>
{
}