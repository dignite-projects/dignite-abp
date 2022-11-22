using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Files.Fakes;

public class FakeFileStore : IFileStore<FakeFile>, ITransientDependency
{
    public Task<bool> BlobNameExistsAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(false);
    }

    public Task CreateAsync(FakeFile blobInfo, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(FakeFile blobInfo, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task<FakeFile> FindByBlobNameAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
    {
        return Task.FromResult<FakeFile>(null);
    }

    public Task<FakeFile> FindByMd5Async(string containerName, string md5, CancellationToken cancellationToken = default)
    {
        return Task.FromResult<FakeFile>(null);
    }

    public Task<bool> Md5ExistsAsync(string containerName, string md5, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(false);
    }

    public Task<bool> ReferencingAnyAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(false);
    }
}