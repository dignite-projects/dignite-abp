
using Dignite.CmsKit.Visits;
using Xunit;

namespace Dignite.CmsKit.MongoDB.Visits;

[Collection(MongoTestCollection.Name)]
public class VisitRepository_Tests : VisitRepository_Tests<CmsKitMongoDbTestModule>
{

}
