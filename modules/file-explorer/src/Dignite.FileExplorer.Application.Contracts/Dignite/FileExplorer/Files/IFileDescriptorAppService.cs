using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;
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
    /// <param name="imageResize"></param>
    /// <returns></returns>
    Task<IRemoteStreamContent> GetStreamAsync([NotNull] string containerName, [NotNull] string blobName, ImageResizeInput imageResize =null);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="containerName"></param>
    /// <returns></returns>
    Task<FileContainerConfigurationDto> GetFileContainerConfigurationAsync([NotNull] string containerName);

    /// <summary>
    /// No user login required to access files that do not require authorization
    /// </summary>
    /// <param name="containerName"></param>
    /// <param name="entityId"></param>
    /// <returns></returns>
    Task<ListResultDto<FileDescriptorDto>> GetListByEntityIdAsync([NotNull] string containerName, [NotNull] string entityId);

    /// <summary>
    /// Users with administrative privileges can delete all files, while users without administrative privileges can only delete the current user's files.
    /// </summary>
    /// <param name="containerName"></param>
    /// <param name="entityId"></param>
    /// <returns></returns>
    Task DeleteByEntityIdAsync([NotNull] string containerName, [NotNull] string entityId);

    /*
    TODO:
    1:Volume statistics by tenant group
    2:Volume statistics by container group
    3:Volume statistics by user group
    4:Calculate the Volume of the specified tenant
    4:Calculate the Volume of the specified user
     */
}