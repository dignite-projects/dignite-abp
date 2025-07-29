using Dignite.Publisher.Categories;
using Xunit;

namespace Dignite.Publisher.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBCategoryDomain_Tests : CategoryManager_Tests<PublisherMongoDbTestModule>
{

}
