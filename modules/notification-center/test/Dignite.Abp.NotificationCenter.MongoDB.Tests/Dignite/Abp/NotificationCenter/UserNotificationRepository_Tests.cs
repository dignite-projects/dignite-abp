using Dignite.Abp.NotificationCenter.MongoDB;
using Xunit;

namespace Dignite.Abp.NotificationCenter;

[Collection(MongoTestCollection.Name)]
public class UserNotificationRepository_Tests : UserNotificationRepository_Tests<AbpNotificationCenterMongoDbTestModule>
{
}