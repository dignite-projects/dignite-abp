using Dignite.Publisher.Categories;
using Xunit;

namespace Dignite.Publisher.MongoDB.Categories;

[Collection(MongoTestCollection.Name)]
public class CategoryRepository_Tests : CategoryRepository_Tests<PublisherMongoDbTestModule>
{
    /* Don't write custom repository tests here, instead write to
     * the base class.
     * One exception can be some specific tests related to MongoDB.
     */
}
