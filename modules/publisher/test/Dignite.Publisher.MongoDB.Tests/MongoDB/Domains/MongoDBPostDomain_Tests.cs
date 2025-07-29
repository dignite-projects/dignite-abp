using Dignite.Publisher.Posts;
using Xunit;

namespace Dignite.Publisher.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBPostDomain_Tests : PostManager_Tests<PublisherMongoDbTestModule>
{

}
