using System.IO;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.BlobStoring;

namespace Dignite.Abp.Files;
public static class FileManagerExtensions
{
    public static async Task<TFile> CreateAsync<TFile, TFileStore>(
        this FileManager<TFile, TFileStore> fileManager,
            TFile file,
            byte[] bytes,
            bool overrideExisting = false,
            CancellationToken cancellationToken = default)
        where TFile : class, IFile
        where TFileStore : IFileStore<TFile>
    {
        using (var memoryStream = new MemoryStream(bytes))
        {
            return await fileManager.CreateAsync(
                file,
                memoryStream,
                overrideExisting,
                cancellationToken
            );
        }
    }


    public static async Task<byte[]> GetAllBytesOrNullAsync<TFile, TFileStore>(
        this FileManager<TFile, TFileStore> fileManager,
        [NotNull] string containerName, 
        [NotNull] string blobName, 
        CancellationToken cancellationToken = default)
        where TFile : class, IFile
        where TFileStore : IFileStore<TFile>
    {
        var stream = await fileManager.GetStreamOrNullAsync(containerName,blobName, cancellationToken);
        if (stream == null)
        {
            return null;
        }

        using (stream)
        {
            return await stream.GetAllBytesAsync(cancellationToken);
        }
    }

    public static async Task<byte[]> GetAllBytesOrNullAsync<TFile, TFileStore,TContainer>(
        this FileManager<TFile, TFileStore> fileManager,
        [NotNull] string blobName,
        CancellationToken cancellationToken = default)
        where TFile : class, IFile
        where TFileStore : IFileStore<TFile>
        where TContainer : class
    {
        var stream = await fileManager.GetStreamOrNullAsync<TContainer>(blobName, cancellationToken);
        if (stream == null)
        {
            return null;
        }

        using (stream)
        {
            return await stream.GetAllBytesAsync(cancellationToken);
        }
    }
}
