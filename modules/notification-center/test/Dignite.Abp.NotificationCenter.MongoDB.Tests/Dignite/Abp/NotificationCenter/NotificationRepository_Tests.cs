using Dignite.Abp.NotificationCenter.MongoDB;
using Xunit;

namespace Dignite.Abp.NotificationCenter;

[Collection(MongoTestCollection.Name)]
public class NotificationRepository_Tests : NotificationRepository_Tests<DigniteAbpNotificationCenterMongoDbTestModule>
{
}