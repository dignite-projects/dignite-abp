using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dignite.Abp.BlobStoring.InfoPersistent;

public interface IBlobInfoStore<TBlobInfo> : ITransientDependency
    where TBlobInfo : IBlobInfo
{
    Task<bool> ExistsAsync(string containerName, string blobName, CancellationToken cancellationToken = default);

    Task<TBlobInfo> FindAsync(string containerName, string blobName, CancellationToken cancellationToken = default);

    Task<bool> HashExistsAsync(string containerName, string hash, CancellationToken cancellationToken = default);

    Task<TBlobInfo> FindByHashAsync(string containerName, string hash, CancellationToken cancellationToken = default);

    /// <summary>
    /// Query if there is a blob referenced by another blob
    /// </summary>
    /// <param name="containerName"></param>
    /// <param name="blobName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> ReferenceExistsAsync(string containerName, string blobName, CancellationToken cancellationToken = default);

    Task CreateAsync(TBlobInfo blobInfo, CancellationToken cancellationToken = default);

    Task DeleteAsync(TBlobInfo blobInfo, CancellationToken cancellationToken = default);
}
