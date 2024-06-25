using System.Threading.Tasks;
using Dignite.Abp.BlobStoring;
using Dignite.FileExplorer.Files;

namespace Dignite.FileExplorer.TestObjects;

public class TestFileDescriptorEntityAuthorizationHandler : FileDescriptorEntityAuthorizationHandlerBase<TestEntity>
{
    protected override Task<TestEntity> GetResourceAsync(FileDescriptor file)
    {
        var resource = new TestEntity();
        return Task.FromResult(resource);
    }
}