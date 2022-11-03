using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.Files;

public interface IFileStore<TFile> : ITransientDependency
    where TFile : IFile
{
    Task<bool> BlobNameExistsAsync(string containerName, string blobName, CancellationToken cancellationToken = default);

    Task<bool> Md5ExistsAsync(string containerName, string md5, CancellationToken cancellationToken = default);

    Task<bool> ReferencingAnyAsync(string containerName, string blobName, CancellationToken cancellationToken = default);

    Task<TFile> FindByBlobNameAsync(string containerName, string blobName, CancellationToken cancellationToken = default);

    Task<TFile> FindByMd5Async(string containerName, string md5, CancellationToken cancellationToken = default);

    Task CreateAsync(TFile blobInfo, bool autoSave = false, CancellationToken cancellationToken = default);

    Task DeleteAsync(TFile blobInfo, bool autoSave = false, CancellationToken cancellationToken = default);
}