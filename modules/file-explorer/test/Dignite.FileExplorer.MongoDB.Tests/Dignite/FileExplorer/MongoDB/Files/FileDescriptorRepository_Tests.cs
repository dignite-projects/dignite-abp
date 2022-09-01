using Dignite.FileExplorer.Samples;
using Xunit;

namespace Dignite.FileExplorer.MongoDB.Samples;

[Collection(MongoTestCollection.Name)]
public class FileDescriptorRepository_Tests : FileDescriptorRepository_Tests<FileExplorerMongoDbTestModule>
{
    /* Don't write custom repository tests here, instead write to
     * the base class.
     * One exception can be some specific tests related to MongoDB.
     */
}
