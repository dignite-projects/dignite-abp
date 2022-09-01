using System.Threading;
using System.Threading.Tasks;
using Dignite.Abp.BlobStoring.InfoPersistent;
using Volo.Abp.Domain.Services;

namespace Dignite.Abp.FileManagement.Files;

public class FileDescriptorStore : DomainService, IBlobInfoStore<FileDescriptor>
{
    private readonly IFileDescriptorRepository _blobRepository;

    public FileDescriptorStore(
        IFileDescriptorRepository blobRepository)
    {
        _blobRepository = blobRepository;
    }

    public async Task<bool> ExistsAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
    {
        return await _blobRepository.ExistsAsync(containerName, blobName, cancellationToken);
    }

    public async Task<bool> HashExistsAsync(string containerName, string hash, CancellationToken cancellationToken = default)
    {
        return await _blobRepository.HashExistsAsync(containerName, hash, cancellationToken);
    }

    public async Task CreateAsync(FileDescriptor file, CancellationToken cancellationToken = default)
    {
        await _blobRepository.InsertAsync(file, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(FileDescriptor blobInfo, CancellationToken cancellationToken = default)
    {
        await _blobRepository.DeleteAsync(blobInfo, cancellationToken: cancellationToken);
    }

    public async Task<FileDescriptor> FindAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
    {
        return await _blobRepository.FindAsync(containerName, blobName, cancellationToken);
    }

    public async Task<FileDescriptor> FindByHashAsync(string containerName, string hash, CancellationToken cancellationToken = default)
    {
        return await _blobRepository.FindByBlobHashAsync(containerName, hash, cancellationToken);
    }

    public async Task<bool> ReferenceExistsAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
    {
        return await _blobRepository.ReferenceExistsAsync(containerName, blobName, cancellationToken);
    }
}
