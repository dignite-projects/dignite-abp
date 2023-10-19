using Xunit;

namespace Dignite.Abp.UserPoints.MongoDB;

[Collection(MongoTestCollection.Name)]
public class UserPointsOrderRepository_Tests : UserPointsOrderRepository_Tests<UserPointsMongoDbTestModule>
{
    /* Don't write custom repository tests here, instead write to
     * the base class.
     * One exception can be some specific tests related to MongoDB.
     */
}
