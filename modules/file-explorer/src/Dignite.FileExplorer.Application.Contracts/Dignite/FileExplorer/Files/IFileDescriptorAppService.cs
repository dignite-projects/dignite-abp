using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Dignite.FileExplorer.Files;

public interface IFileDescriptorAppService : ICrudAppService<FileDescriptorDto, Guid, GetFilesInput, CreateFileInput, UpdateFileInput>
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="containerName"></param>
    /// <param name="blobName"></param>
    /// <returns></returns>
    Task<IRemoteStreamContent> GetStreamAsync([NotNull] string containerName, [NotNull] string blobName);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="containerName"></param>
    /// <returns></returns>
    Task<BlobHandlerConfigurationDto> GetBlobHandlerConfiguration([NotNull] string containerName);

    /*
    TODO:
    1:Volume statistics by tenant group
    2:Volume statistics by container group
    3:Volume statistics by user group
    4:Calculate the Volume of the specified tenant
    4:Calculate the Volume of the specified user
     */
}