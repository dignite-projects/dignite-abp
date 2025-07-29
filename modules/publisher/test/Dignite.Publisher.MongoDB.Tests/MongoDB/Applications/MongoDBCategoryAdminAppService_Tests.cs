using Dignite.Publisher.MongoDB;
using Dignite.Publisher.Categories;
using Xunit;

namespace Dignite.Publisher.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDBCategoryAdminAppService_Tests : CategoryAdminAppService_Tests<PublisherMongoDbTestModule>
{

}
