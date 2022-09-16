using Dignite.FileExplorer.Files;
using Xunit;

namespace Dignite.FileExplorer.MongoDB.Files;

[Collection(MongoTestCollection.Name)]
public class FileDescriptorRepository_Tests : FileDescriptorRepository_Tests<FileExplorerMongoDbTestModule>
{
    /* Don't write custom repository tests here, instead write to
     * the base class.
     * One exception can be some specific tests related to MongoDB.
     */
}