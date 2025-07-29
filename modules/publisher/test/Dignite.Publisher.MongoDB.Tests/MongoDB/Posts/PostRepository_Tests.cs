using Dignite.Publisher.Posts;
using Xunit;

namespace Dignite.Publisher.MongoDB.Posts;

[Collection(MongoTestCollection.Name)]
public class PostRepository_Tests : PostRepository_Tests<PublisherMongoDbTestModule>
{
    /* Don't write custom repository tests here, instead write to
     * the base class.
     * One exception can be some specific tests related to MongoDB.
     */
}
