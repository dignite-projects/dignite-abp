using Dignite.Publisher.MongoDB;
using Dignite.Publisher.Posts;
using Xunit;

namespace Dignite.Publisher.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDBPostAdminAppService_Tests : PostAdminAppService_Tests<PublisherMongoDbTestModule>
{

}
