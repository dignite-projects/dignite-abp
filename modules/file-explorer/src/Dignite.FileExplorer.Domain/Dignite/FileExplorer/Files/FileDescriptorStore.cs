using System.Threading;
using System.Threading.Tasks;
using Dignite.Abp.Files;
using Volo.Abp.Domain.Services;

namespace Dignite.FileExplorer.Files;

public class FileDescriptorStore : DomainService, IFileStore<FileDescriptor>
{
    private readonly IFileDescriptorRepository _blobRepository;

    public FileDescriptorStore(
        IFileDescriptorRepository blobRepository)
    {
        _blobRepository = blobRepository;
    }

    public async Task<bool> BlobNameExistsAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
    {
        return await _blobRepository.BlobNameExistsAsync(containerName, blobName, cancellationToken);
    }

    public async Task<bool> Md5ExistsAsync(string containerName, string md5, CancellationToken cancellationToken = default)
    {
        return await _blobRepository.Md5ExistsAsync(containerName, md5, cancellationToken);
    }

    public async Task<bool> ReferencingAnyAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
    {
        return await _blobRepository.ReferencingAnyAsync(containerName, blobName, cancellationToken);
    }

    public async Task CreateAsync(FileDescriptor file, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        await _blobRepository.InsertAsync(file, autoSave, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(FileDescriptor blobInfo, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        await _blobRepository.DeleteAsync(blobInfo, autoSave, cancellationToken: cancellationToken);
    }

    public async Task<FileDescriptor> FindByBlobNameAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
    {
        return await _blobRepository.FindByBlobNameAsync(containerName, blobName, cancellationToken);
    }

    public async Task<FileDescriptor> FindByMd5Async(string containerName, string md5, CancellationToken cancellationToken = default)
    {
        return await _blobRepository.FindByMd5Async(containerName, md5, cancellationToken);
    }
}