using System;
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

    public async Task<bool> BlobNameExistsAsync(string containerName, string blobName, Guid? ignoredId = null, CancellationToken cancellationToken = default)
    {
        return await _blobRepository.BlobNameExistsAsync(containerName, blobName, ignoredId, cancellationToken);
    }

    public async Task CreateAsync(FileDescriptor file, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        await _blobRepository.InsertAsync(file, autoSave, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(FileDescriptor blobInfo, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        await _blobRepository.DeleteAsync(blobInfo, autoSave, cancellationToken: cancellationToken);
    }

    public async Task<FileDescriptor> FindAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
    {
        return await _blobRepository.FindByBlobNameAsync(containerName, blobName, cancellationToken);
    }
}