using System.Threading.Tasks;
using Dignite.Abp.BlobStoring;
using Dignite.FileExplorer.Files;
using FileExplorerSample.Entities;
using Volo.Abp.DependencyInjection;

namespace FileExplorerSample.Services;

public class SampleEntityResourceAuthorizationHandler : FileDescriptorEntityAuthorizationHandlerBase<SampleEntity>, ITransientDependency
{
    protected override Task<SampleEntity> GetResourceAsync(FileDescriptor file)
    {
        var resource = new SampleEntity();
        return Task.FromResult(resource);
    }
}