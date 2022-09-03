using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Files;

public interface IFileStore<TBlobInfo> : ITransientDependency
    where TBlobInfo : IFile
{
    Task<bool> BlobNameExistsAsync(string containerName, string blobName, Guid? ignoredId = null, CancellationToken cancellationToken = default);

    Task<TBlobInfo> FindAsync(string containerName, string blobName, CancellationToken cancellationToken = default);

    Task CreateAsync(TBlobInfo blobInfo, bool autoSave=false, CancellationToken cancellationToken = default);

    Task DeleteAsync(TBlobInfo blobInfo, bool autoSave = false, CancellationToken cancellationToken = default);
}
