using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Files.Fakes;
public class FakeFileStore : IFileStore<FakeFile>, ITransientDependency
{
    public Task<bool> BlobNameExistsAsync(string containerName, string blobName, Guid? ignoredId = null, CancellationToken cancellationToken = default)
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

    public Task<FakeFile> FindAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
    {
        return Task.FromResult<FakeFile>(null);
    }
}
