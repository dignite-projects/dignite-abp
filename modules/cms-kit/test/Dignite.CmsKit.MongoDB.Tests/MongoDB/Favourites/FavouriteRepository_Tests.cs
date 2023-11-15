
using Dignite.CmsKit.Favourites;
using Xunit;

namespace Dignite.CmsKit.MongoDB.Favourites;

[Collection(MongoTestCollection.Name)]
public class FavouriteRepository_Tests : FavouriteRepository_Tests<CmsKitMongoDbTestModule>
{

}
